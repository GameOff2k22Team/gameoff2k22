using UnityEngine;

public class HealEnemy : BossEnemy
{
    [SerializeField]
    private AK.Wwise.Event SFXHeal = null;
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D player))
        {
            player.GetComponent<Player2D>().AddHP(_damage);
            _isAlive = false;
            SFXHeal.Post(gameObject);
        }
        else if (col.TryGetComponent<DespawnArea>(out DespawnArea area))
        {
            _isAlive = false;
        }
    }
}

