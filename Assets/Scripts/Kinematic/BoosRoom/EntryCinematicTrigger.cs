using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EntryCinematicTrigger : MonoBehaviour
{
    public PlayableDirector entryCinematic ;
    private CinematicManager _CinematicManager;
    [SerializeField]
    private AK.Wwise.Event SFXCinematic = null;

    private void Start()
    {
        _CinematicManager = CinematicManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            _CinematicManager.StartCinemactic(entryCinematic);
            SFXCinematic.Post(gameObject);
        }
    }
}
