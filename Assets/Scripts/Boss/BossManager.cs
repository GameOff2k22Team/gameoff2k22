using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [SerializeField]
    private NewBouncerSplitEnemy splitterEnemy;

    public static List<BossEnemy> enemies = new List<BossEnemy>();

    private float _projectileSpeed = 3.0f;

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

    [Header("Phase 2")]
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP2> p2S1Pattern;
    [SerializeField]
    private int _numberOfPatternP2S1 = 1;
    private float _phase2ModifierSpawnSpeed = 5;

    [Header("Phase 3")]
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP3> p3S1Pattern;
    [SerializeField]
    private int _numberOfPatternP3S1 = 1;

    private void Start()
    {
        UpdateBossPhase(BossState.phase3);
    }

    #region Generic Method
    private void SpawnEnemy(EnemyType enemyType ,Transform tr, Vector3 direction, int projectileSpeedRatio, float freq, float amp)
    {
        switch (enemyType)
        {
            case EnemyType.normal:
                BossEnemy _currentEnemy = Instantiate(enemy, tr.position, Quaternion.identity);
                _currentEnemy.Initialize(direction, _projectileSpeed * ((float)projectileSpeedRatio / 100f), _currentDamage);
                break;
            case EnemyType.zigzag:
                ZigZagEnemy _currentZigEnemy = Instantiate(zigZagEnemy, tr.position, Quaternion.identity);
                _currentZigEnemy.Initialize(direction, _projectileSpeed * ((float)projectileSpeedRatio / 100f), _currentDamage, freq, amp);
                break;
            case EnemyType.splitter:
                NewBouncerSplitEnemy _currentSplitEnemy = Instantiate(splitterEnemy, tr.position, Quaternion.identity);
                _currentSplitEnemy.Initialize(direction, _projectileSpeed * ((float)projectileSpeedRatio / 100f), _currentDamage, true);
                break;
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
                StartCoroutine(Phase2Coroutine());
                break;
            case BossState.phase3:
                StartCoroutine(Phase3Coroutine());
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

    #region P2 Boss
    private IEnumerator Phase2Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P2PatternCoroutine(p2S1Pattern, _numberOfPatternP2S1, true, _spawningSpeed * _phase2ModifierSpawnSpeed));
    }

    private IEnumerator P2PatternCoroutine(List<PatternBossP2> BossPatterns, int numberOfPattern, bool isRandom, float spawningSpeed)
    {
        var i = 0;
        while (i < numberOfPattern)
        {
            PatternBossP2 bossPattern = isRandom ? BossPatterns[UnityEngine.Random.Range(0, BossPatterns.Count)] : BossPatterns[i];

            foreach (SpawnManager.P2SpawnArea spawnArea in bossPattern.SpawnArea)
            {
                SpawnManager.BossP2SpawnPattern spawnAreaPosition = SpawnManager.Instance.GetSpawnAreaPositionByTypeP2(spawnArea);
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


    #region P3 Boss

    private IEnumerator Phase3Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P3PatternCoroutine(p3S1Pattern, _numberOfPatternP3S1, false));
    }

    private IEnumerator P3PatternCoroutine(List<PatternBossP3> BossPatterns, int numberOfPattern, bool isRandom)
    {
        var i = 0;
        while (i < numberOfPattern)
        {
            PatternBossP3 currentBossPattern = BossPatterns[i];
            LineAreaManager.Instance.EnableLines(currentBossPattern.nbOfVertLines, currentBossPattern.nbOfHorLines);
            i += 1;
            yield return new WaitForSecondsRealtime(currentBossPattern.timeToWait);
        }
    }
    
    #endregion
    // Refaire avec sinusoides et amplitude
    // P2 Zones qui rebondissent et qui te rentrent dedans
    // P3 lasers  )))))))))))))))))))))))))))))))
    // 2 ligne en abscisse et ordonnée
    // E2 : 3 Lignes 2 carrés de libre
    // E3 : 4 Lignes 1 carré de libre



}
