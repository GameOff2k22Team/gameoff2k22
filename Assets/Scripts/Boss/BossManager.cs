using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private BossEnemy enemy;

    public static List<BossEnemy> enemies = new List<BossEnemy>();

    [SerializeField]
    private Transform TopSpawnerCenter;

    [SerializeField]
    private Transform TopSpawnerLeft;

    [SerializeField]
    private Transform TopSpawnerRight;

    [SerializeField]
    private Transform BottomSpawnerCenter;

    [SerializeField]
    private Transform BottomSpawnerLeft;

    [SerializeField]
    private Transform BottomSpawnerRight;

    [SerializeField]
    private Transform LeftSpawnerUp;

    [SerializeField]
    private Transform LeftSpawnerMiddle;

    [SerializeField]
    private Transform LeftSpawnerDown;

    [SerializeField]
    private Transform RightSpawnerUp;

    [SerializeField]
    private Transform RightSpawnerMiddle;

    [SerializeField]
    private Transform RightSpawnerDown;

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
        yield return StartCoroutine(DownPatternCoroutine());
        yield return StartCoroutine(UpPatternCoroutine());
        yield return StartCoroutine(LeftPatternCoroutine());
        yield return StartCoroutine(RightPatternCoroutine());
        yield return StartCoroutine(UpDownPatternCoroutine());
        yield return StartCoroutine(LeftRightPatternCoroutine());
    }

    private IEnumerator DownPatternCoroutine()
    {
        SpawnEnemy(TopSpawnerCenter, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(TopSpawnerLeft, Vector3.down);
        SpawnEnemy(TopSpawnerRight, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(TopSpawnerCenter, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(TopSpawnerLeft, Vector3.down);
        SpawnEnemy(TopSpawnerRight, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);

    }

    private IEnumerator UpDownPatternCoroutine()
    {
        SpawnEnemy(TopSpawnerCenter, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(BottomSpawnerLeft, Vector3.up);
        SpawnEnemy(BottomSpawnerRight, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(TopSpawnerCenter, Vector3.down);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(BottomSpawnerLeft, Vector3.up);
        SpawnEnemy(BottomSpawnerRight, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);

    }

    private IEnumerator UpPatternCoroutine()
    {
        SpawnEnemy(BottomSpawnerCenter, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(BottomSpawnerLeft, Vector3.up);
        SpawnEnemy(BottomSpawnerRight, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(BottomSpawnerCenter, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(BottomSpawnerLeft, Vector3.up);
        SpawnEnemy(BottomSpawnerRight, Vector3.up);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
    }

    private IEnumerator LeftPatternCoroutine()
    {
        SpawnEnemy(RightSpawnerMiddle, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(RightSpawnerUp, Vector3.left);
        SpawnEnemy(RightSpawnerDown, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(RightSpawnerMiddle, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(RightSpawnerUp, Vector3.left);
        SpawnEnemy(RightSpawnerDown, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
    }

    private IEnumerator RightPatternCoroutine()
    {
        SpawnEnemy(LeftSpawnerMiddle, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(LeftSpawnerUp, Vector3.right);
        SpawnEnemy(LeftSpawnerDown, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(LeftSpawnerMiddle, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(LeftSpawnerUp, Vector3.right);
        SpawnEnemy(LeftSpawnerDown, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
    }

    private IEnumerator LeftRightPatternCoroutine()
    {
        SpawnEnemy(LeftSpawnerMiddle, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(RightSpawnerUp, Vector3.left);
        SpawnEnemy(RightSpawnerDown, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(LeftSpawnerMiddle, Vector3.right);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
        SpawnEnemy(RightSpawnerUp, Vector3.left);
        SpawnEnemy(RightSpawnerDown, Vector3.left);
        yield return new WaitForSecondsRealtime(_spawningSpeed);
    }


    private enum BossState
    {
        phase1,
        phase2,
        phase3,
    }
}
