using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossP1Pattern", menuName = "Pattern/BossP1", order = 1)]
public class PatternBossP1 : ScriptableObject
{
    public List<SpawnManager.P1SpawnArea> SpawnArea;
    public int speed = 100;
    public int size = 100;
}