using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseObject : MonoBehaviour
{
    [Serializable]
    public struct ObjectToUseInfo
    {
        public GoToReturnFromInventory objectToUse;
        public Vector3 position;
        public Vector3 rotation;
    }

    public List<ObjectToUseInfo> objectsToUse;

    public UnityEvent OnUsedObject;

    public void UseObjects()
    {
        GoToReturnFromInventory[] objects = GetObjectsToReturn();

        if (InventoryManager.Instance.AreObjectsInInventory(objects))
        {
            foreach (ObjectToUseInfo usedObject in objectsToUse)
            {
                InventoryManager.Instance.RemoveObjectFromInventory(usedObject.objectToUse, 
                                                                    usedObject.position,
                                                                    usedObject.rotation);
            }

            OnUsedObject?.Invoke();
        }
        
    }

    private GoToReturnFromInventory[] GetObjectsToReturn()
    {
        List<GoToReturnFromInventory> gos = new List<GoToReturnFromInventory>();
        
        foreach(ObjectToUseInfo obj in objectsToUse)
        {
            gos.Add(obj.objectToUse);
        }

        return gos.ToArray();
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
