using UnityEngine;
using UnityEngine.Events;

public class PlayerFaceObject : MonoBehaviour
{
    public string playerTag;
    public UnityEvent onObjectFacePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (isConditionToNextLevelValidated(other.gameObject))
            {
                onObjectFacePlayer?.Invoke();
            }
        }
    }

    private bool isConditionToNextLevelValidated(GameObject go)
    {
        bool areObjectsFacing = Vector3.Dot(this.transform.forward, 
                                            go.transform.forward) < 0;

        return areObjectsFacing;
    }
}
