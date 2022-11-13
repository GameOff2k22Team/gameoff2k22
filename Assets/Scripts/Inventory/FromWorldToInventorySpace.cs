using System.Collections;
using UnityEngine;

public class FromWorldToInventorySpace : MonoBehaviour
{
    public Transform inventorySlot;
    public float timeToAnimate;

    private void OnEnable()
    {
        
    }

    IEnumerator ChangePositionRotationScale(Vector3 newPosition)
    {
        float time = 0;

        while (time < timeToAnimate)
        {
            Vector3.Lerp(transform.position, inventorySlot.position, time/timeToAnimate);
            yield return true;
        }
    } 
}
