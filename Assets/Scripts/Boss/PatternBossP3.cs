using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossP3Pattern", menuName = "Pattern/BossP3", order = 1)]
public class PatternBossP3 : ScriptableObject
{
    public List<int> nbOfHorLines = new List<int>();
    public List<int> nbOfVertLines = new List<int>();
    public float timeForActivation = 1.5f;
    public float timeActivated = 1f;
    public float timeToWait = 4f;
}