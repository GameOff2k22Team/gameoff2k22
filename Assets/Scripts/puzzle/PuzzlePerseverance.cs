using System.Collections;
using UnityEngine;

public class PuzzlePerseverance : PuzzleBase
{
    [Header("Falling chest")]
    public AnimationClip fallAnimationClip;

    public Animator animator;
    public AnimationClip openAnimation;

    public bool isAlreadyInRoom = false;
    public float minimumTimeToWait = 0.5f;
    public float maximumTimeToWait = 3;

    protected override void Start()
    {
        base.Start();
        type = PuzzleManager.PuzzleType.PERSEVERANCE;

        if (!isAlreadyInRoom)
        {
            StartCoroutine(MakeChestFall());
        }
    }

    IEnumerator MakeChestFall()
    {
        float originalAnimatorSpeed = animator.speed;

        animator.enabled = true;
        animator.Play(fallAnimationClip.name, 0, 0);
        animator.speed = 0;

        float timeToWait = GetTimeToWait();

        yield return new WaitForSecondsRealtime(timeToWait);
        animator.speed = originalAnimatorSpeed;
    }

    private float GetTimeToWait()
    {
        return Random.Range(minimumTimeToWait, maximumTimeToWait);
    }

    protected override bool CheckIfCanBeOpened()
    {
        return base.CheckIfCanBeOpened() &&
            !this._puzzleManager.perseverancePuzzleIsEnd;
    }
}
