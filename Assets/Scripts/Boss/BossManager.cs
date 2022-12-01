using Architecture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private enum BossState { phase1 = 1, 
                             phase2 = 2, 
                             phase3 = 3,
                             phase4 = 4,}

    [SerializeField]
    private GameEventListener OnPlayerDeath;

    [SerializeField]
    private GameEvent OnPlayerMustNotDie;

    [SerializeField]
    private Animator MotherP12;

    [SerializeField]
    private Animator MotherP3;

    [SerializeField]
    private BossEnemy enemy;

    [SerializeField]
    private ZigZagEnemy zigZagEnemy;

    [SerializeField]
    private NewBouncerSplitEnemy splitterEnemy;

    [SerializeField]
    private HealEnemy healEnemy;

    public static List<BossEnemy> enemies = new List<BossEnemy>();

    private float _projectileSpeed = 5.0f;

    private float _spawningSpeed = 1.0f;

    private float _spawningSpeedP4 = 3.0f;

    private float _timerBeforeStart = 3.0f;

    private int _numberOfEnemies;

    private int _currentDamage;


    [Header("Phase 1")]
    [SerializeField]
    private GameEvent OnPhase1End;
    [Header("Step 1")]
    [SerializeField]
    private bool isP1S1Random;
    [SerializeField]
    private List<PatternBossP1> p1S1Pattern;
    [SerializeField]
    private int _numberOfPatternP1S1 = 1;
    [Header("Step 2")]
    [SerializeField]
    private bool isP1S2Random;
    [SerializeField]
    private List<PatternBossP1> p1S2Pattern;
    [SerializeField]
    private int _numberOfPatternP1S2 = 10;
    [Header("Step 3")]
    [SerializeField]
    private bool isP1S3Random;
    [SerializeField]
    private List<PatternBossP1> p1S3Pattern;
    [SerializeField]
    private int _numberOfPatternP1S3 = 10;
    [SerializeField, Range(0f, 5f)]
    private float _P1S3SpawningSpeed;
    [Header("Step 4")]
    [SerializeField]
    private bool isP1S4Random;
    [SerializeField]
    private List<PatternBossP1> p1S4Pattern;
    [SerializeField]
    private int _numberOfPatternP1S4 = 10;
    [SerializeField, Range(0f, 5f)]
    private float _P1S4SpawningSpeed;

    [Space(10)]
    [Header("Phase 2")]
    [SerializeField]
    private GameEvent OnPhase2End;
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP2> p2S1Pattern;
    [SerializeField]
    private int _numberOfPatternP2S1 = 1;
    private float _phase2ModifierSpawnSpeed = 5;
    [SerializeField, Range(0f, 5f)]
    private float _P2S1SpawningSpeed;
    [Header("Step 2")]
    [SerializeField]
    private List<PatternBossP2> p2S2Pattern;
    [SerializeField]
    private int _numberOfPatternP2S2 = 1;
    [SerializeField, Range(0f, 5f)]
    private float _P2S2SpawningSpeed;
    [Header("Step 3")]
    [SerializeField]
    private List<PatternBossP2> p2S3Pattern;
    [SerializeField]
    private int _numberOfPatternP2S3 = 1;
    [SerializeField, Range(0f, 5f)]
    private float _P2S3SpawningSpeed;

    [Space(10)]
    [Header("Phase 3")]
    [SerializeField]
    private GameEvent OnPhase3End;
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP3> p3S1Pattern;
    [SerializeField]
    private int _numberOfPatternP3S1 = 1;
    [SerializeField]
    private List<PatternBossP3> p3S2Pattern;
    [SerializeField]
    private List<PatternBossP3> p3S3Pattern;
    [SerializeField]
    private List<PatternBossP3> p3S4Pattern;

    [Space(10)]
    [Header("Phase 4")]
    [SerializeField]
    private GameEvent OnPhase4End;
    [Header("Step 1")]
    [SerializeField]
    private List<PatternBossP1> p4S1Pattern;


    private void Awake()
    {
        OnPlayerDeath.RegisterListener(CallWhenPlayerDies);
    }

    private void OnDisable()
    {
        OnPlayerDeath.UnregisterListener(CallWhenPlayerDies);
    }

    private void CallWhenPlayerDies()
    {
        MotherP12.SetBool("isAttacking", false);
        ClearEnemies();
        StopAllCoroutines();
    }

    private void Start()
    {
        //UpdateBossPhase(BossState.phase3);
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
            case EnemyType.heal:
                HealEnemy _currentHealEnemy = Instantiate(healEnemy, tr.position, Quaternion.identity);
                _currentHealEnemy.Initialize(direction, _projectileSpeed * ((float)projectileSpeedRatio / 100f), _currentDamage);
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
                ClearEnemies();
                StartCoroutine(Phase2Coroutine());
                break;
            case BossState.phase3:
                ClearEnemies();
                StartCoroutine(Phase3Coroutine());
                break;
            case BossState.phase4:
                ClearEnemies();
                StartCoroutine(Phase4Coroutine());
                break;
        }
    }

    public void UpdateBossPhase(int stateIdx)
    {
        BossState bossState = (BossState)stateIdx;
        UpdateBossPhase(bossState);
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
    private static void ClearEnemies()
    {
        
        foreach (BossEnemy enemy in enemies)
        {
            if (enemy.gameObject != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    #region P1 Boss

    [SerializeField]
    private AK.Wwise.Event SFXProjectileP1 = null;
    private IEnumerator Phase1Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P1PatternCoroutine(p1S1Pattern, _numberOfPatternP1S1, isP1S1Random, _spawningSpeed));
        yield return StartCoroutine(WaitBeforeStart());

        yield return StartCoroutine(P1PatternCoroutine(p1S2Pattern, _numberOfPatternP1S2, isP1S2Random, _spawningSpeed));
        yield return StartCoroutine(WaitBeforeStart());

        yield return StartCoroutine(P1PatternCoroutine(p1S3Pattern, _numberOfPatternP1S3, isP1S3Random, _P1S3SpawningSpeed));

        yield return StartCoroutine(P1PatternCoroutine(p1S4Pattern, _numberOfPatternP1S4, isP1S4Random, _P1S4SpawningSpeed));
        yield return StartCoroutine(WaitBeforeStart());

        OnPhase1End?.Raise();
    }



    private IEnumerator P1PatternCoroutine(List<PatternBossP1> BossPatterns, int numberOfPattern, bool isRandom, float spawningSpeed)
    {
        var i = 0;
        
        int countNumber = isRandom ? numberOfPattern : BossPatterns.Count;
        while (i < countNumber)
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
                SFXProjectileP1.Post(gameObject);
            }

            i += 1;
            yield return new WaitForSeconds(spawningSpeed);
        }
        
        

    }
    #endregion

    #region P2 Boss

    [SerializeField]
    private AK.Wwise.Event SFXProjectileP2 = null;
    private IEnumerator Phase2Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        MotherP12.SetBool("isAttacking", true);
        yield return StartCoroutine(P2PatternCoroutine(p2S1Pattern, _numberOfPatternP2S1, false, _P2S1SpawningSpeed));
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(P2PatternCoroutine(p2S2Pattern, _numberOfPatternP2S2, false, _P2S2SpawningSpeed));
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(P2PatternCoroutine(p2S3Pattern, _numberOfPatternP2S3, false, _P2S3SpawningSpeed));
        MotherP12.SetBool("isAttacking", false);
        yield return new WaitForSeconds(10f);
        ClearEnemies();
        yield return new WaitForSeconds(2f);
        OnPhase2End?.Raise();
    }

    private IEnumerator P2PatternCoroutine(List<PatternBossP2> BossPatterns, int numberOfPattern, bool isRandom, float spawningSpeed)
    {
        var i = 0;
        int countNumber = isRandom ? numberOfPattern : BossPatterns.Count;
        while (i < countNumber)
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
                SFXProjectileP2.Post(gameObject);
            }

            i += 1;
            yield return new WaitForSeconds(spawningSpeed);
        }

    }

    #endregion


    #region P3 Boss

    [SerializeField]
    private AK.Wwise.Event SFXProjectileP3 = null;
    private IEnumerator Phase3Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P3PatternCoroutine(p3S1Pattern));
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P3PatternCoroutine(p3S2Pattern));
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P3PatternCoroutine(p3S3Pattern));
        yield return StartCoroutine(WaitBeforeStart());
        OnPlayerMustNotDie?.Raise();
        yield return StartCoroutine(P3PatternCoroutine(p3S4Pattern));
        OnPhase3End?.Raise();
    }

    private IEnumerator P3PatternCoroutine(List<PatternBossP3> BossPatterns)
    {
        var i = 0;
        while (i < BossPatterns.Count)
        {
            MotherP3.SetTrigger("Attack");
            PatternBossP3 currentBossPattern = BossPatterns[i];
            SFXProjectileP3.Post(gameObject);
            LineAreaManager.Instance.ToggleLines(currentBossPattern.nbOfVertLines, 
                currentBossPattern.nbOfHorLines, 
                currentBossPattern.timeForActivation,
                currentBossPattern.timeActivated);
            i += 1;
            yield return new WaitForSeconds(currentBossPattern.timeToWait);
        }
    }

    #endregion

    [SerializeField]
    private AK.Wwise.Event SFXProjectileP4 = null;

    private IEnumerator Phase4Coroutine()
    {
        yield return StartCoroutine(WaitBeforeStart());
        yield return StartCoroutine(P4PatternCoroutine(p4S1Pattern, _numberOfPatternP1S1, isP1S1Random, _spawningSpeedP4));
        OnPhase4End?.Raise();
    }



    private IEnumerator P4PatternCoroutine(List<PatternBossP1> BossPatterns, int numberOfPattern, bool isRandom, float spawningSpeed)
    {
        var i = 0;

        int countNumber = isRandom ? numberOfPattern : BossPatterns.Count;
        while (i < countNumber)
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
                SFXProjectileP4.Post(gameObject);

            }

            i += 1;
            yield return new WaitForSeconds(spawningSpeed);
        }



    }
    // Refaire avec sinusoides et amplitude
    // P2 Zones qui rebondissent et qui te rentrent dedans
    // P3 lasers  )))))))))))))))))))))))))))))))
    // 2 ligne en abscisse et ordonn�e
    // E2 : 3 Lignes 2 carr�s de libre
    // E3 : 4 Lignes 1 carr� de libre



}
