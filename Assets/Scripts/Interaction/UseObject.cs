using System;
using System.Collections.Generic;
using UnityEngine;

public class UseObject : MonoBehaviour, Interaction
{
    [Serializable]
    public struct ObjectToUseInfo
    {
        public GoToReturnFromInventory objectToUse;
        public Vector3 position;
    }

    public List<ObjectToUseInfo> objectsToUse;

    public void Interact()
    {
        foreach (ObjectToUseInfo usedObject in objectsToUse)
        {
            InventoryManager.Instance.RemoveObjectFromInventory(usedObject.objectToUse, 
                                                                usedObject.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (ObjectToUseInfo usedObject in objectsToUse)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(usedObject.position, 0.1f);
        }
    }
}
