using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAreaDamage : MonoBehaviour
{
    private float _timeBeforeDamage = 2.0f;
    private float _timeWithDamage = 2.0f;
    private Renderer _renderer;
    private BoxCollider2D _boxCollider;
    private int _damage;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = false;

    }

    public void OnEnable()
    {
        StartCoroutine(AppearCoroutine());
    }

    public void Initialize(float timeBeforeDamage, float timeWithDamage)
    {
        _timeBeforeDamage = timeBeforeDamage;
        
        _timeWithDamage = timeWithDamage;

    }

    public IEnumerator AppearCoroutine()
    {
        _renderer.material.color = new Color(1f, 1f, 1f, 0.5f);

        float timer = 0f;
        while (timer < _timeBeforeDamage)
        {

            timer += Time.deltaTime;
            animator.SetFloat("speed1", timer);
            yield return null;
        }
        _renderer.material.color = new Color(1f, 1f, 1f, 1f);

        _boxCollider.enabled = true;
        timer = 0f;
        while (timer < _timeWithDamage)
        {
            timer += Time.deltaTime;
            animator.SetFloat("speed2", timer);

            yield return null;
        }
        _boxCollider.enabled = false;
        gameObject.SetActive(false);
        
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D player))
        {
            player.GetComponent<Player2D>().RemoveHP(_damage);

        }
    }
}
