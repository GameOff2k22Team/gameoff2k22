using UnityEngine;
using UnityEngine.Playables;

public class PlayableLaunch : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public void LaunchPlayable()
    {
        CinematicManager.Instance.StartCinemactic(playableDirector);
    }
}
