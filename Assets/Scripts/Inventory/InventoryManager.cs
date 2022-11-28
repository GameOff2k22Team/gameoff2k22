using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public GameObject inventory;

    public Transform[] inventorySlotIcons;
    private Dictionary<GoToReturnFromInventory, Transform> linkSlotObject 
        = new Dictionary<GoToReturnFromInventory, Transform>();
    
    public override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);

        GameManager.OnGameStateChanged += UpdateInventoryDisplayOnGameManagerStateChanged;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateInventoryDisplayOnGameManagerStateChanged;
    }

    private void UpdateInventoryDisplayOnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.OnStartKinematic:
                HideInventory();
                break;
            case GameState.OnEndKinematic:
                DisplayInventory();
                break;
        }
    }
        
    public void DisplayInventory()
    {
        inventory.SetActive(true);
    }

    public void HideInventory()
    {
        inventory.SetActive(false);
    }

    public void PlaceObjectInInventory(GoToReturnFromInventory objectToPlace)
    {
        Transform slot = FindFreeSlot();

        objectToPlace.PutObjectInInventory(slot);

        linkSlotObject.Add(objectToPlace, slot);
    }

    public void RemoveObjectFromInventory(GoToReturnFromInventory objectToRemove, 
                                          Vector3 position, Vector3 rotation)
    {
        objectToRemove.RemoveObjectFromInventory(position, rotation);

        bool objectIsRemoved = linkSlotObject.Remove(objectToRemove);

        if (!objectIsRemoved)
        {
            Debug.LogError("The object " + objectToRemove.name + " was not in the inventory", 
                            objectToRemove);
        }
    }

    public bool AreObjectsInInventory(GoToReturnFromInventory[] objectsToCheck)
    {
        foreach (GoToReturnFromInventory obj in objectsToCheck)
        {
            if (!linkSlotObject.ContainsKey(obj))
            {
                return false;
            }
        }
        return true;
    }

    private Transform FindFreeSlot()
    {
        foreach (Transform icon in inventorySlotIcons)
        {
            if (!linkSlotObject.ContainsValue(icon))
            {
                return icon;
            }
        }

        Debug.LogError("Inventory is too short");
        throw new IndexOutOfRangeException();
    }
}
