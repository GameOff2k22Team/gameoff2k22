using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossP2Pattern", menuName = "Pattern/BossP2", order = 1)]
public class PatternBossP2 : ScriptableObject
{
    public EnemyType enemyType;
    public List<SpawnManager.P2SpawnArea> SpawnArea;
    public int speed = 100;
    public int size = 100;
    public float freq = 2f;
    public float amp = 0.05f;
}