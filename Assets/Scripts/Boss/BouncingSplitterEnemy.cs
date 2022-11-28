using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BouncingSplitterEnemy : BossEnemy
{
    private bool _isSpawning;
    public bool _isFirst = true;
    private Vector3 _lastNormal = Vector3.zero;
    private Vector3 _lastDirection = Vector3.zero;

    public int SplitCount;
    private int _bounceCount = 0;
    private int _bounceCountSplit = 5;

    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        if(_isFirst)
        {
            _direction = new Vector3(1f, 1f, 0).normalized;
        }
        _movementSpeed = 2;
        while (_isAlive)
        {
            this.transform.position += _direction * _movementSpeed * Time.deltaTime;
            yield return null;
        }
        //Destroy(this.gameObject);
    }


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if (col.TryGetComponent<ZigZagWall>(out ZigZagWall wall) )
        {
            _lastNormal = wall.NormalVector;
            _bounceCount += 1;
            
            if (!_isSpawning)
            {
                if (Mathf.Abs(Vector3.Dot(_direction, _lastNormal)) < 0.1f)
                {

                }
                else
                {
                    _lastDirection = _direction;//+ Random.Range(0.0f, 0.16f) * new Vector3(1f, 1f, 0f);
                    _direction = Vector3.Reflect(_direction, _lastNormal);// + Random.Range(0.0f, 0.16f) * new Vector3(1f, 1f, 0f);
                    if (_bounceCount == _bounceCountSplit)
                    {
                        _isSpawning = true;
                        StartCoroutine(SplitCoroutine());
                    }
                }
                
            }
        }
    }

    

    protected IEnumerator SplitCoroutine()
    {
        
        yield return new WaitForSeconds(0.1f);
        _isFirst = false;
        if(SplitCount < 4)
        {
            SplitCount += 1;
            var newObj = Instantiate(this, this.transform.position, this.transform.rotation);
            var newObj2 = Instantiate(this, this.transform.position, this.transform.rotation);
            newObj.SplitCount = SplitCount;
            newObj2.SplitCount = SplitCount;
            newObj.transform.localScale /= 1.25f;
            newObj2.transform.localScale /= 1.25f;

            newObj2.SplitCount = SplitCount;
            newObj.Initialize((-_lastDirection ).normalized, _movementSpeed, _damage);
            newObj2.Initialize((_direction).normalized, _movementSpeed, _damage);

            newObj._isFirst = false;
            newObj2._isFirst = false;

            _isSpawning = false;
            Destroy(this.gameObject);

        }
        else
        {
            _isSpawning = false;
        }


    }
}
