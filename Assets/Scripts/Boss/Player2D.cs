using Architecture;
using System.Collections;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    [SerializeField]
    private GameEvent OnPlayer2DDeath;

    [SerializeField]
    private GameEventListener OnPlayerMustNotDie;

    [SerializeField]
    private GameEvent PlayerHPChanged;

    private int _playerHP = 5;

    private bool _isInvincible = false;

    private float _invincibilityTimer = 2.0f;

    private Renderer _renderer;

    private bool _playerMustNotDie;

    private int PlayerHP
    {
        get => _playerHP;
        set
        {
            _playerHP = value;
            PlayerHPChanged.Raise<int>(_playerHP);
        }
    }

    private void Awake()
    {
        _renderer  = GetComponent<Renderer>();
        OnPlayerMustNotDie.RegisterListener(SetPlayerMustNotDie);

    }

    private void OnDestroy()
    {
        OnPlayerMustNotDie.UnregisterListener(SetPlayerMustNotDie);

    }

    private void SetPlayerMustNotDie()
    {
        _playerMustNotDie = true;
    }
    public void RemoveHP(int damage)
    {

        if (!_isInvincible && _playerMustNotDie)
        {
            //_isInvincible = true;
            StartCoroutine(DamageInvincibilityCoroutine());

            PlayerHP = 0;
            return;
        }
        else if(!_isInvincible && PlayerHP > 1)
        {
            //_isInvincible = true;

            PlayerHP -= 1;
            StartCoroutine(DamageInvincibilityCoroutine());
        }

        else if (PlayerHP == 1)
        {
            OnPlayer2DDeath.Raise();
            PlayerHP = 5;
        }
    }

    public void AddHP(int heal)
    {
        if(PlayerHP < 5)
        {
            PlayerHP += 1;
        }
    }

    private IEnumerator DamageInvincibilityCoroutine()
    {
        _isInvincible = true;
        _renderer.material.color = new Color(_renderer.material.color.r,
                                               _renderer.material.color.g,
                                               _renderer.material.color.b,
                                               0.3f);
        yield return new WaitForSeconds(_invincibilityTimer);
        _renderer.material.color = new Color(_renderer.material.color.r,
                                               _renderer.material.color.g,
                                               _renderer.material.color.b,
                                               1f);
        _isInvincible = !_isInvincible;
    }
}
