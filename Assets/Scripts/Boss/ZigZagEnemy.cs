using System.Collections;
using UnityEngine;

public class ZigZagEnemy : BossEnemy
{
    protected float _frequency = 2f;

    protected float _amplitude = 0.05f;

    public void Initialize(Vector3 dir, float movSpeed, int dmg, float freq, float amplitude)
    {
        base.Initialize(dir, movSpeed, dmg);
        _frequency = freq; 
        _amplitude = amplitude; 

    }

    protected override IEnumerator Start()
    {
        float time = 0f;
        _movementSpeed /= 4;
        Vector3 oscillationVector = (_direction.y == 0) ? new Vector3(0, 1f, 0) : new Vector3(1f, 0, 0);
        while (_isAlive)
        {
            this.transform.position += _direction * _movementSpeed * Time.deltaTime
                + oscillationVector * _amplitude * Mathf.Cos(2 * Mathf.PI * _frequency * time);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}