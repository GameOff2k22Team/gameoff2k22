using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{

    public enum PuzzleType { PERSEVERANCE, SPEED, FOCUS }

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
    [Header("Perseverance Puzzle")]
    public int perseverancePuzzleNumberOfTryRequired = 15;
    public PerseverancePuzzle perseverancePuzzle;
    [HideInInspector]
    public bool perseverancePuzzleIsEnd = false;
    [Space(10)]
    [Header("Focus Puzzle")]
    [SerializeField]
    private GameObject _sceneLight;
    [SerializeField]
    private GameObject _lightingRoad;
    [SerializeField]
    private List<GameObject> _focusKeys;
    [SerializeField]
    private GameObject _fakeKey;

    // Start is called before the first frame update
    void Start()
    {
        perseverancePuzzle = new PerseverancePuzzle(PuzzleType.PERSEVERANCE, perseverancePuzzleNumberOfTryRequired);
        if(_lightingRoad)
            this.EnsureLightingRoadIsActive();
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
            case PuzzleType.SPEED:
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
