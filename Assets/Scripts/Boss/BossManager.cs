using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private BossEnemy enemy;

    [SerializeField]
    private List<Spawner> P1SpawerPatern;

    public static List<BossEnemy> enemies = new List<BossEnemy>();

    private float _projectileSpeed = 7.0f;

    private float _spawningSpeed = 1.0f;

    private int _numberOfEnemies;

    private int _currentDamage;

    private void Start()
    {
        UpdateBossPhase(BossState.phase1);
    }
    private void SpawnEnemy(Transform tr, Vector3 direction)
    {
        BossEnemy _currentEnemy = Instantiate(enemy, tr.position, Quaternion.identity);
        _currentEnemy.Initialize(direction, _projectileSpeed, _currentDamage);
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

    private IEnumerator Phase1Coroutine()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss start: 3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss start: 2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss start: 1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Boss start: GO!");
        yield return StartCoroutine(P1PatternCoroutine());
        yield return StartCoroutine(P1PatternCoroutine());
        yield return StartCoroutine(P1PatternCoroutine());
        yield return StartCoroutine(P1PatternCoroutine());
        yield return StartCoroutine(P1PatternCoroutine());
        yield return StartCoroutine(P1PatternCoroutine());
    }

    private IEnumerator P1PatternCoroutine()
    {
        Spawner patern = GetRandomPatern();
        foreach (SpawnManager.P1SpawnArea spawnArea in patern.SpawnArea)
        {
            SpawnManager.BossP1SpawnPosition spawnAreaPosition = SpawnManager.Instance.GetSpawnAreaPositionByType(spawnArea);
            SpawnEnemy(spawnAreaPosition.position, spawnAreaPosition.direction);
        }
        yield return new WaitForSecondsRealtime(_spawningSpeed);
    }

    private Spawner GetRandomPatern()
    {
        return P1SpawerPatern[UnityEngine.Random.Range(0, P1SpawerPatern.Count)];
    }

    private enum BossState
    {
        phase1,
        phase2,
        phase3,
    }
}
