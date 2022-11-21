using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Transform Spawner1;

    private int HP;

    private float _timeResetHurt;

    private float _projectileSpeed;

    private float _spawningSpeed;

    private int _numberOfEnemies;

    private List<GameObject> enemies = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnEnemy(Spawner1);
    }

    private void SpawnEnemy()
    {

    }

    private void SpawnEnemy(Transform tr)
    {
        Instantiate(enemy, tr.position, Quaternion.identity);
    }

    private void UpdateBossPhase()
    {

    }

    private enum BossState
    {
        phase1,
        phase2,
        phase3,
    }
}
