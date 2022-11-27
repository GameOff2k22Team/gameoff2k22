using System.Collections;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    private int _playerHP;

    private bool _isInvincible = false;

    private float _invincibilityTimer = 2.0f;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer  = GetComponent<Renderer>();
    }
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
        _renderer.material.color = new Color(_renderer.material.color.r,
                                               _renderer.material.color.g,
                                               _renderer.material.color.b,
                                               0.5f);
        yield return new WaitForSeconds(_invincibilityTimer);
        _renderer.material.color = new Color(_renderer.material.color.r,
                                               _renderer.material.color.g,
                                               _renderer.material.color.b,
                                               1f);
        _isInvincible = !_isInvincible;
    }
}
