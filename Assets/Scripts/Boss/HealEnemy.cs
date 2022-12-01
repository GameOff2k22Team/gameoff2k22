using UnityEngine;

public class HealEnemy : BossEnemy
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D player))
        {
            player.GetComponent<Player2D>().AddHP(_damage);
            _isAlive = false;
        }
        else if (col.TryGetComponent<DespawnArea>(out DespawnArea area))
        {
            _isAlive = false;
        }
    }
}

