using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BossManager : MonoBehaviour
{
    private enum BossState { phase1, phase2, phase3 }

    [SerializeField]
    private BossEnemy enemy;

    [SerializeField]
    private ZigZagEnemy zigZagEnemy;

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
    [Header("Step 2")]
    [SerializeField]
    private List<PatternBossP1> p1S3Pattern;
    [SerializeField]
    private int _numberOfPatternP1S3 = 10;

    private void Start()
    {
        UpdateBossPhase(BossState.phase1);
    }

    #region Generic Method
    private void SpawnEnemy(EnemyType enemyType ,Transform tr, Vector3 direction, int projectileSpeedRatio, float freq, float amp)
    {
        if(enemyType == EnemyType.normal)
        {
            BossEnemy _currentEnemy = Instantiate(enemy, tr.position, Quaternion.identity);
            _currentEnemy.Initialize(direction, _projectileSpeed * (projectileSpeedRatio / 100), _currentDamage);
        }
        else
        {
            ZigZagEnemy _currentEnemy = Instantiate(zigZagEnemy, tr.position, Quaternion.identity);
            _currentEnemy.Initialize(direction, _projectileSpeed * (projectileSpeedRatio / 100), _currentDamage, freq, amp);
        }
        
    }

    private void UpdateBossPhase(BossState state)
    {
        switch (state)
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
        yield return StartCoroutine(P1PatternCoroutine(p1S1Pattern, _numberOfPatternP1S1, true, _spawningSpeed));
        yield return StartCoroutine(P1PatternCoroutine(p1S2Pattern, _numberOfPatternP1S2, true, _spawningSpeed));
        yield return StartCoroutine(P1PatternCoroutine(p1S3Pattern, _numberOfPatternP1S3, false, _spawningSpeed * 2));
        UpdateBossPhase(BossState.phase2);
    }

    private IEnumerator P1PatternCoroutine(List<PatternBossP1> BossPatterns, int numberOfPattern, bool isRandom, float spawningSpeed)
    {
        var i = 0;
        while(i < numberOfPattern)
        {
            PatternBossP1 bossPattern = isRandom ? BossPatterns[UnityEngine.Random.Range(0, BossPatterns.Count)] : BossPatterns[i];
            
            foreach (SpawnManager.P1SpawnArea spawnArea in bossPattern.SpawnArea)
            {
                SpawnManager.BossP1SpawnPattern spawnAreaPosition = SpawnManager.Instance.GetSpawnAreaPositionByType(spawnArea);
                SpawnEnemy(bossPattern.enemyType, 
                    spawnAreaPosition.position, 
                    spawnAreaPosition.direction, 
                    bossPattern.speed,
                    bossPattern.freq, 
                    bossPattern.amp);
            }

            i += 1;
            yield return new WaitForSecondsRealtime(spawningSpeed);
        }

    }
    #endregion


    // Refaire avec sinusoides et amplitude
    // P2 Zones qui rebondissent et qui te rentrent dedans
    // P3 lasers  )))))))))))))))))))))))))))))))
    // 2 ligne en abscisse et ordonnée
    // E2 : 3 Lignes  2 carrés de libre
    // E3 : 4 Lignes 1 carré de libre



}
