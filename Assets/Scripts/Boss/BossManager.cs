using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private enum BossState { phase1, phase2, phase3 }

    [SerializeField]
    private BossEnemy enemy;
    public static List<BossEnemy> enemies = new List<BossEnemy>();

    private float _projectileSpeed = 7.0f;

    private float _spawningSpeed = 1.0f;

    private float _timerBeforeStart = 3.0f;

    private int _numberOfEnemies;

    private int _currentDamage;


    [Header("Phase 1")]
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP1> p1S1Pattern;
    [SerializeField]
    private int _numberOfPatternP1S1 = 1;
    [Header("Step 2")]
    [SerializeField]
    private List<PatternBossP1> p1S2Pattern;
    [SerializeField]
    private int _numberOfPatternP1S2 = 10;

    private void Start()
    {
        UpdateBossPhase(BossState.phase1);
    }

    #region Generic Method
    private void SpawnEnemy(Transform tr, Vector3 direction, int projectileSpeedRatio)
    {
        BossEnemy _currentEnemy = Instantiate(enemy, tr.position, Quaternion.identity);
        _currentEnemy.Initialize(direction, _projectileSpeed * (projectileSpeedRatio/100), _currentDamage);
    }

    private void UpdateBossPhase(BossState state)
    {
        switch (state
)
        {
            case BossState.phase1:
                StartCoroutine(Phase1Coroutine());
                break;
            case BossState.phase2:
                break;
            case BossState.phase3:
                break;
        }
    }

    private IEnumerator WaitBeforeStart()
    {
        var i = 0;
        while (i < _timerBeforeStart)
        {
            Debug.Log("Start in" + i);
            yield return new WaitForSeconds(1.0f);
            i += 1;
        }

    }
    #endregion

    #region P1 Boss
    private IEnumerator Phase1Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P1PatternCoroutine(p1S1Pattern, _numberOfPatternP1S1));
        yield return StartCoroutine(P1PatternCoroutine(p1S2Pattern, _numberOfPatternP1S2));
    }

    private IEnumerator P1PatternCoroutine(List<PatternBossP1> BossPattern, int numberOfPattern)
    {
        var i = 0;
        while(i < numberOfPattern)
        {
            PatternBossP1 randomBossPattern = BossPattern[UnityEngine.Random.Range(0, BossPattern.Count)];
            foreach (SpawnManager.P1SpawnArea spawnArea in randomBossPattern.SpawnArea)
            {
                SpawnManager.BossP1SpawnPattern spawnAreaPosition = SpawnManager.Instance.GetSpawnAreaPositionByType(spawnArea);
                SpawnEnemy(spawnAreaPosition.position, spawnAreaPosition.direction, randomBossPattern.speed);
            }
            i += 1;
            yield return new WaitForSecondsRealtime(_spawningSpeed);
        }

    }
    #endregion




}
