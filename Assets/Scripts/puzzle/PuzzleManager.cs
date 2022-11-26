using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PuzzleManager : MonoBehaviour
{

    public enum PuzzleType { PERSEVERANCE, BABA, FOCUS}
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
    private GameObject _sceneLight;
    [SerializeField]
    private GameObject _lightingRoad;
    [SerializeField]
    private List<GameObject> _focusKeys;
    [SerializeField]
    private GameObject _fakeKey;
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
    private const int NUMBER_OF_PUZZLE = 2;
    private bool canGoToNextRoom = false;
    private Transform spawnSpot;

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
        if(_lightingRoad)
            this.EnsureLightingRoadIsActive();
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
        loadManagerAnimator.SetTrigger(FADE_OUT_TRIGGER);
        LoadManager.Instance.OnFadeOutCompleted.AddListener(FadeOut);
    }

    public void SetNextPuzzle()
    {
        int nextTypeIdx = ((int)currentPuzzleType + 1) % NUMBER_OF_PUZZLE;
        PuzzleType nextType = (PuzzleType)nextTypeIdx;

        RespawnPlayer();

        SetPuzzleType(nextType);
    }

    private int GetNextPuzzleIdx()
    {
        return ((int)currentPuzzleType + 1) % 2;
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
                this.GiveArtefact(chest);
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
            this.AddTry();
        }
    }

    private void AddTry()
    {
        this.perseverancePuzzle.numberOfTry += 1;
    }
    
    private void GiveArtefact(PuzzleBase chest)
    {
        chest.artefact.SetActive(true);
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

    public void ClickOnLightSwitch()
    {
        this.ShowRandomKey();
        this.EnsureLightingRoadIsNotActive();
        this._fakeKey.SetActive(false);
        _sceneLight.SetActive(false);
        var timeLeft = 5.0f;
        StartCoroutine(this.TimerBeforeReset(timeLeft));
    }

    public void ResetFocusEnigme()
    {
        this.EnsureLightingRoadIsActive();
        _sceneLight.SetActive(true);
        this._fakeKey.SetActive(true);
        this.HideAllKeys();
    }

    private void EnsureLightingRoadIsActive()
    {
        _lightingRoad.SetActive(true);
    }

    private void EnsureLightingRoadIsNotActive()
    {
        _lightingRoad.SetActive(false);
    }

    private void ShowRandomKey()
    {
        var selectedKey = Random.Range(0, _focusKeys.Count);
        _focusKeys[selectedKey].SetActive(true);
    }

    private void HideAllKeys()
    {
        foreach(GameObject key in _focusKeys)
        {
            key.SetActive(false);
        }
    }
    IEnumerator TimerBeforeReset(float timeLeft)
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;

        }
        this.ResetFocusEnigme();
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