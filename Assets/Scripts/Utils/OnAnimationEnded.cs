using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnAnimationEnded : MonoBehaviour
{
    public UnityEvent OnAnimationEndedEvent;
    public UnityEvent OnAnimationEndedEventWithDelay;

    /// Oui je sais c'est moche je suis d�sol�e j'ai pas trouv� mieux!
    public void AnimationEnded()
    {
        OnAnimationEndedEvent?.Invoke();
        StartCoroutine(AnimationEndWithDelay());
    }

    IEnumerator AnimationEndWithDelay()
    {
        yield return null;
        OnAnimationEndedEventWithDelay?.Invoke();
    }
}
