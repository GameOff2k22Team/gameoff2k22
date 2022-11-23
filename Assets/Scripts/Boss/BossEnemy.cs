using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private Vector3 _direction;

    private float _movementSpeed;

    private bool _isAlive = false;

    private int _damage = 1;

    private void Awake()
    {
        BossManager.enemies.Add(this);
        _isAlive = true;
    }

    private void OnDisable()
    {
        BossManager.enemies.Remove(this);
    }

    public void Initialize(Vector3 dir, float movSpeed, int dmg)
    {
        _direction = dir;
        _movementSpeed = movSpeed;
        _damage = dmg;
    }

    public IEnumerator Start()
    {
        while(_isAlive)
        {
            this.transform.position += _direction * _movementSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D player))
        {
            player.GetComponent<Player2D>().RemoveHP(_damage);
            _isAlive = false;
        }
        else if(col.TryGetComponent<DespawnArea>(out DespawnArea area))
        {
            Debug.Log("isHittingDespawn");
            _isAlive=false;
        }
    }
}
