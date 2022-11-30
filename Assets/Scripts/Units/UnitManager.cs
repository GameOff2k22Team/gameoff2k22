using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : MonoBehaviour
{
    [HideInInspector]
    public enum UnitType { PLAYER, MOTHER, FAIRY , FAKE_FAIRY_FOREST, FAIRY_BOSS, FAIRY_BOSS_PLAYER}
    
    [Serializable]
    public struct UnitByType
    {
        public UnitType type;
        public GameObject unit;
    }

    public List<UnitByType> listOfUnitByType;
    private Dictionary<UnitType, GameObject> _unitByType = new Dictionary<UnitType, GameObject>();

    public static UnitManager Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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
