﻿using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    protected bool isRotated;

    protected Vector3 _direction;

    protected float _movementSpeed;

    protected bool _isAlive = false;

    protected int _damage = 1;

    protected virtual void Awake()
    {
        BossManager.enemies.Add(this);
        _isAlive = true;
    }

    protected void OnDestroy()
    {
        BossManager.enemies.Remove(this);
    }

    public virtual void Initialize(Vector3 dir, float movSpeed, int dmg)
    {
        _direction = dir;
        _movementSpeed = movSpeed;
        _damage = dmg;
    }

    protected virtual IEnumerator Start()
    {
        while(_isAlive)
        {
            if(isRotated)
            {
                float angle = Vector3.SignedAngle(transform.right, _direction, transform.right);
                this.transform.Rotate(transform.forward, angle);
            }    
            this.transform.position += _direction * _movementSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D player))
        {
            player.GetComponent<Player2D>().RemoveHP(_damage);
        }
        else if(col.TryGetComponent<DespawnArea>(out DespawnArea area))
        {
            _isAlive=false;
        }
    }
}

public enum EnemyType
{
    normal,
    zigzag,
    splitter,
    heal,
}

