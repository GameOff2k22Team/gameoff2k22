using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : Singleton<UnitManager>
{
    [HideInInspector]
    public enum UnitType { PLAYER, MOTHER, FAIRY }
    
    [Serializable]
    public struct UnitByType
    {
        public UnitType type;
        public GameObject unit;
    }

    public List<UnitByType> listOfUnitByType;
    private Dictionary<UnitType, GameObject> _unitByType = new Dictionary<UnitType, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (UnitByType unitByType in listOfUnitByType)
        {
            _unitByType.Add(unitByType.type, unitByType.unit);
        }
    }

    public GameObject GetUnitByType(UnitType unitType)
    {
        return _unitByType[unitType];
    }
}
