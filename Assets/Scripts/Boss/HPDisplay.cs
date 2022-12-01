using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField]
    private GameEventListener<int> OnPlayerHPChange;
    [SerializeField]
    private RawImage[] _healthImage;

    private int _currentHPIdx;
    private int _maximumHPIdx;

    [SerializeField]
    private AK.Wwise.Event SFXHit = null;
    [SerializeField]
    private AK.Wwise.Event SFXDead = null;

    private void Awake()
    {
        OnPlayerHPChange.RegisterListener(UpdateHPDisplay);
        _maximumHPIdx = _healthImage.Length - 1;
        _currentHPIdx = _maximumHPIdx;
    }

    private void UpdateHPDisplay(int nbrOfLives)
    {
        Debug.Log("nb of lives " +nbrOfLives);
        Debug.Log("current hp" +  _currentHPIdx);

        bool hasPlayerLooseLife = nbrOfLives < _currentHPIdx + 1;
        if (hasPlayerLooseLife)
        {
            for (int i = _currentHPIdx; i > nbrOfLives - 1; i--)
            {
                _healthImage[i].enabled = false;
                SFXHit.Post(gameObject);
            }            
        } else
        {
            for (int i = _currentHPIdx +1 ; i < nbrOfLives; i++)
            {
                _healthImage[i].enabled = true;
                SFXDead.Post(gameObject);
            }
        }
        _currentHPIdx = nbrOfLives - 1;
    }
}
