using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    #region Boss P1 Attributes
    public enum P1SpawnArea { TOPLEFT, TOPMIDDLE, TOPRIGHT, LEFTUP, LEFTMIDDLE, LEFTDOWN, DOWNLEFT, DOWNMIDDLE, DOWNRIGHT, RIGHTUP, RIGHTMIDDLE, RIGHTDOWN };

    [Serializable]
    public struct BossP1SpawnPattern
    {
        public P1SpawnArea area;
        public Transform position;
        public Vector3 direction;

        public BossP1SpawnPattern(P1SpawnArea _area, Transform _position, Vector3 _direction)
        {
            this.area = _area;
            this.position = _position;
            this.direction = _direction;
        }
    }

    [Header("Boss P1")]
    [SerializeField]
    private List<BossP1SpawnPattern> _bossP1Positions = new() { 
        new BossP1SpawnPattern(P1SpawnArea.TOPLEFT, null, Vector3.down) ,
        new BossP1SpawnPattern(P1SpawnArea.TOPMIDDLE, null, Vector3.down) ,
        new BossP1SpawnPattern(P1SpawnArea.TOPRIGHT, null, Vector3.down) ,

        new BossP1SpawnPattern(P1SpawnArea.LEFTUP, null, Vector3.right) ,
        new BossP1SpawnPattern(P1SpawnArea.LEFTMIDDLE, null, Vector3.right) ,
        new BossP1SpawnPattern(P1SpawnArea.LEFTDOWN, null, Vector3.right) ,

        new BossP1SpawnPattern(P1SpawnArea.DOWNLEFT, null, Vector3.up) ,
        new BossP1SpawnPattern(P1SpawnArea.DOWNMIDDLE, null, Vector3.up) ,
        new BossP1SpawnPattern(P1SpawnArea.DOWNRIGHT, null, Vector3.up) ,

        new BossP1SpawnPattern(P1SpawnArea.RIGHTUP, null, Vector3.left) ,
        new BossP1SpawnPattern(P1SpawnArea.RIGHTMIDDLE, null, Vector3.left) ,
        new BossP1SpawnPattern(P1SpawnArea.RIGHTDOWN, null, Vector3.left) ,
    };

    private Dictionary<P1SpawnArea, BossP1SpawnPattern> _spawnPatternByArea = new Dictionary<P1SpawnArea, BossP1SpawnPattern>();
    #endregion

    private void OnEnable()
    {
        foreach (BossP1SpawnPattern spawnPosition in _bossP1Positions)
        {
            _spawnPatternByArea.Add(spawnPosition.area, spawnPosition);
        }
    }
    #region BossP1 Function
    public BossP1SpawnPattern GetSpawnAreaPositionByType(P1SpawnArea p1SpawnArea)
    {
        return _spawnPatternByArea[p1SpawnArea];
    }
    #endregion

}
