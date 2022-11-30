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
    public bool useOnlyOnce;

    public UnityEvent OnUsedObject;
    public UnityEvent OnObjectNotInInventory;
    private bool alreadyUsed = false;

    public void UseObjects()
    {
        GoToReturnFromInventory[] objects = GetObjectsToReturn();
        bool isInInventory = InventoryManager.Instance.AreObjectsInInventory(objects);

        if (!useOnlyOnce || !alreadyUsed)
        {
            if (isInInventory)
            {
                foreach (ObjectToUseInfo usedObject in objectsToUse)
                {
                    InventoryManager.Instance.RemoveObjectFromInventory(usedObject.objectToUse, 
                                                                        usedObject.position,
                                                                        usedObject.rotation);
                }

                alreadyUsed = true;
                OnUsedObject?.Invoke();
            }
            else
            {
                OnObjectNotInInventory?.Invoke();
            }
        } 
        
    }

    public void AddObjectToUse(GoToReturnFromInventory objectToReturn, Vector3 objPosition, Vector3 objRotation)
    {
        ObjectToUseInfo objectToUseInfo = new ObjectToUseInfo() { objectToUse = objectToReturn,
                                                                  position = objPosition,
                                                                  rotation = objRotation};
        objectsToUse.Add(objectToUseInfo);
    }

    public void AddObjectToUse(GoToReturnFromInventory objectToReturn)
    {
        Vector3 position = Vector3.zero; 
        Vector3 rotation = Vector3.zero; 
        if (objectsToUse.Count != 0)
        {
            position = objectsToUse[0].position;
            rotation = objectsToUse[0].rotation;
        }
        AddObjectToUse(objectToReturn, position, rotation);
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
