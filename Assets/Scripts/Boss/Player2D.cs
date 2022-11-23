using System.Collections;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private int _playerHP;

    private bool _isInvincible = false;

    private float _invincibilityTimer = 2.0f;
    public void RemoveHP(int damage)
    {
        if(!_isInvincible)
        {
            _playerHP -= damage;
            StartCoroutine(DamageInvincibilityCoroutine());
        }
    }

    private IEnumerator DamageInvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityTimer);
        _isInvincible = !_isInvincible;
    }
}
