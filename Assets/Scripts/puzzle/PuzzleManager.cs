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
        this.EnsureLightingRoadIsActive();
    }

    public void OpenChest(PuzzleType puzzleType, Chest chest)
    {
        switch (puzzleType)
        {
            case PuzzleType.PERSEVERANCE:
                this.OpenChestPerseverance(chest);
                break;
            case PuzzleType.FOCUS:
                this.GiveArtefact();
                break;
            default: break;
        }
    }

    private void OpenChestPerseverance(Chest chest)
    {
        chest.isOpen = true;
        if (this.perseverancePuzzle.numberOfTry == this.perseverancePuzzle.numberOfTryRequired)
        {
            this.GiveArtefact();
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
    
    private void GiveArtefact()
    {
        Debug.Log("GiveArtefact");
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
