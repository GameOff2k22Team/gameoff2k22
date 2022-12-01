using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PuzzleManager : MonoBehaviour
{

    public enum PuzzleType {PERSEVERANCE, FOCUS, BABA }
    public static PuzzleManager Instance { get; private set; }

    public struct PerseverancePuzzle
    {
        public PuzzleType type;
        public int numberOfTryRequired;
        public int numberOfTry;

        public PerseverancePuzzle(PuzzleType pType, int nbTryRequried)
        {
            this.type = pType;
            this.numberOfTryRequired = nbTryRequried;
            this.numberOfTry = 1;
        }

    }

    public Transform playerTransform;
    public Interaction leftDoor;
    public Interaction rightDoor;

    [Header("Perseverance Puzzle")]
    public GameObject perseverancePuzzleObject;
    public int perseverancePuzzleNumberOfTryRequired = 15;
    public PerseverancePuzzle perseverancePuzzle;
    [HideInInspector]
    public bool perseverancePuzzleIsEnd = false;
    [Space(10)]
    [Header("Focus Puzzle")]
    public GameObject focusPuzzleObject;
    [SerializeField]
    private GameObject _hiddenChest;
    [SerializeField]
    private Transform _spawnPoint;
    [Space(10)]
    [Header("Baba Puzzle")]
    public GameObject babaPuzzleObject;
    [Space(10)]
    public UnityEvent OnHasAllArtefactPieces;

    private Dictionary<PuzzleType, GameObject> puzzleObjects = new Dictionary<PuzzleType, GameObject>();
    private PuzzleType currentPuzzleType;
    private Animator loadManagerAnimator;
    private const string FADE_IN_TRIGGER = "FadeIn";
    private const string FADE_OUT_TRIGGER = "FadeOut";
    private const int NUMBER_OF_PUZZLE = 3;
    private bool canGoToNextRoom = false;
    private Transform spawnSpot;
    public AkEvent SFXChestPop;
    public AkEvent RTPCDoorClosed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as PuzzleManager;
        }
        else
        {
            Destroy(this.gameObject);
        }
        loadManagerAnimator = LoadManager.Instance.gameObject.GetComponent<Animator>();

        puzzleObjects.Add(PuzzleType.PERSEVERANCE, perseverancePuzzleObject);
        puzzleObjects.Add(PuzzleType.FOCUS, focusPuzzleObject);
        puzzleObjects.Add(PuzzleType.BABA, babaPuzzleObject);

        SetPuzzleType(PuzzleType.PERSEVERANCE);
        
        spawnSpot = new GameObject().transform;
        spawnSpot.position = playerTransform.position;
        spawnSpot.rotation = playerTransform.rotation;
        spawnSpot.localScale = playerTransform.localScale;
    }

    void Start()
    {
        perseverancePuzzle = new PerseverancePuzzle(PuzzleType.PERSEVERANCE, perseverancePuzzleNumberOfTryRequired);
    }

    private void SetPuzzleType(PuzzleType type)
    {
        if (puzzleObjects.TryGetValue(currentPuzzleType, out GameObject currentGOPuzzle))
        {
            currentGOPuzzle.SetActive(false);
        }

        currentPuzzleType = type;

        if (puzzleObjects.TryGetValue(currentPuzzleType, out currentGOPuzzle))
        {
            currentGOPuzzle.SetActive(true);
        }
    }

    public void GoToNextRoom()
    {
        bool isLastPuzzle = GetNextPuzzleIdx() == 0;
        
        if (canGoToNextRoom &&
            !isLastPuzzle)
        {
            NextPuzzle();
            canGoToNextRoom = false;
        }
    }

    public void NextPuzzle()
    {
        rightDoor.UnregisterToAction();
        leftDoor.UnregisterToAction();
        loadManagerAnimator.SetTrigger(FADE_OUT_TRIGGER);
        LoadManager.Instance.OnFadeOutCompleted.AddListener(FadeOut);
        
    }

    public void SetNextPuzzle()
    {
        int nextTypeIdx = ((int)currentPuzzleType + 1) % NUMBER_OF_PUZZLE;
        PuzzleType nextType = (PuzzleType)nextTypeIdx;

        RespawnPlayer();

        SetPuzzleType(nextType);

        RTPCDoorClosed.HandleEvent(gameObject);
    }

    private int GetNextPuzzleIdx()
    {
        return ((int)currentPuzzleType + 1) % 3;
    }

    private void RespawnPlayer()
    {
        playerTransform.gameObject.SetActive(false);
        playerTransform.position = spawnSpot.position;
        playerTransform.rotation = spawnSpot.rotation;
        playerTransform.localScale = spawnSpot.localScale;
        playerTransform.gameObject.SetActive(true);
    }

    private void FadeOut()
    {
        loadManagerAnimator.SetTrigger(FADE_IN_TRIGGER);
        SetNextPuzzle();
        LoadManager.Instance.OnFadeOutCompleted.RemoveListener(FadeOut);
    }

    public void OpenChest(PuzzleType puzzleType, PuzzleBase chest)
    {
        switch (puzzleType)
        {
            case PuzzleType.PERSEVERANCE:
                this.OpenChestPerseverance(chest);
                break;
            case PuzzleType.FOCUS:
                this.OpenFocusChest(chest);
                break;
            case PuzzleType.BABA:
                this.GiveArtefact(chest);
                break;
            default: break;
        }
    }

    private void OpenChestPerseverance(PuzzleBase chest)
    {
        if (this.perseverancePuzzle.numberOfTry == this.perseverancePuzzle.numberOfTryRequired)
        {
            if(!this.perseverancePuzzleIsEnd)
            {
                this.GiveArtefact(chest);
                this.perseverancePuzzleIsEnd = true;
            }
        }
        else
        {
            PuzzlePerseverance chestPerseverence = chest as PuzzlePerseverance;
            chestPerseverence.animator.enabled = true;
            chestPerseverence.animator.Play(chestPerseverence.openAnimation.name);
            this.AddTry();
        }
    }

    private void AddTry()
    {
        this.perseverancePuzzle.numberOfTry += 1;
    }
    
    private void GiveArtefact(PuzzleBase chest)
    {
        chest.openChestWithArtefact.LaunchPlayable();
        canGoToNextRoom = true;

        if(chest.TryGetComponent(out UseObject useObject))
        {
            useObject.UseObjects();
        }

        bool isLastPuzzle = GetNextPuzzleIdx() == 0;
        if (isLastPuzzle)
        {
            OnHasAllArtefactPieces?.Invoke();
        }
    }

    private void OpenFocusChest(PuzzleBase chest)
    {
        PuzzleHidden chestHidden = chest as PuzzleHidden;
        if (!chestHidden._realChest)
        {
            _hiddenChest.transform.position = _spawnPoint.position;
            _hiddenChest.transform.rotation = _spawnPoint.rotation;
            chestHidden._realChest = true;
            SFXChestPop.HandleEvent(gameObject);
           
            
        }
        else
        {
            this.GiveArtefact(chest);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PuzzleManager))]
[CanEditMultipleObjects]
public class PuzzleManagerEditor : Editor
{
    PuzzleManager puzzleManager;
    private void Awake()
    {
        puzzleManager = target as PuzzleManager;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        if (GUILayout.Button("Next Puzzle")) {
            puzzleManager.NextPuzzle();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif