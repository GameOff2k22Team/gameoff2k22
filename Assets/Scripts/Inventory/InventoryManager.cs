using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Transform[] inventorySlotIcons;
    private Dictionary<Transform, FromWorldToInventorySpace> linkSlotObject 
        = new Dictionary<Transform, FromWorldToInventorySpace>();
    
    public override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaceObjectInInventory(FromWorldToInventorySpace objectToPlace)
    {
        Transform slot = FindFreeSlot();

        objectToPlace.PutObjectInInventory(slot);

        linkSlotObject.Add(slot, objectToPlace);
    }

    private Transform FindFreeSlot()
    {
        foreach (Transform icon in inventorySlotIcons)
        {
            if (!linkSlotObject.TryGetValue(icon, out _))
            {
                return icon;
            }
        }

        Debug.LogError("Inventory is too short");
        throw new IndexOutOfRangeException();
    }
}
