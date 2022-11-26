using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossP1Pattern", menuName = "Pattern/BossP1", order = 1)]
public class PatternBossP1 : ScriptableObject
{
    public EnemyType enemyType;
    public List<SpawnManager.P1SpawnArea> SpawnArea;
    public int speed = 100;
    public int size = 100;
    public float freq = 2f;
    public float amp = 0.05f;
}