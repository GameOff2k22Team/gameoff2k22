using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Objet rentre de l'extérieur
// Une fois dedans il rebondit sur les murs
// Au bout de x rebond il se split 1 fois. 
// Quand elle se split, une boule qui reflect avec un random sur -15°/ +15°
// Et une autre qui repart en sens inverse avec -15 /+15 

public class NewBouncerSplitEnemy : BossEnemy
{
    private bool _isBounceActive = false;
    private bool _mustSplit = false;
    private Vector3 _currentNormal = Vector3.zero;
    private Vector3 _lastNormal = Vector3.zero;
    private Vector3 _lastDirection = Vector3.zero;
    private int _numberOfBouncesBeforeSplit = 5;
    private int _numberOfBounces = 0;

    protected new IEnumerator Start()
    {
        while(_isAlive)
        {
            this.transform.position += _direction * _movementSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void Initialize(Vector3 dir, float movSpeed, int dmg, bool mustSplit)
    {
        base.Initialize(dir, movSpeed, dmg);
        _mustSplit = mustSplit;
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isBounceActive)
        {
            if (col.GetComponent<EnemyActivator>() != null)
            {
                _isBounceActive = true;
            }
        }
        else
        {
            if (col.TryGetComponent<ZigZagWall>(out ZigZagWall wall))
            {
                _numberOfBounces += 1;
                
                _currentNormal = wall.NormalVector;
                _lastDirection = _direction;
                _direction = Vector3.Reflect(_direction, _currentNormal);
                _direction = ReturnRandomOnXandY(_direction, _currentNormal);
                
                if(_numberOfBounces == _numberOfBouncesBeforeSplit && _mustSplit)
                {

                    NewBouncerSplitEnemy newEnemy1 = Instantiate(this, this.transform.position - _lastDirection * Time.deltaTime * _movementSpeed, Quaternion.identity);
                    NewBouncerSplitEnemy newEnemy2 = Instantiate(this, this.transform.position + _direction * Time.deltaTime * _movementSpeed, Quaternion.identity);
                    newEnemy1.Initialize(-_lastDirection, this._movementSpeed, this._damage, false);
                    newEnemy2.Initialize(_direction, this._movementSpeed, this._damage,false);
                    newEnemy1.transform.localScale /= 1.2f;
                    newEnemy2.transform.localScale /= 1.2f;
                    Destroy(this.gameObject);

                }
            }
        }

    }

    private Vector3 ReturnRandomOnXandY(Vector3 direction, Vector3 normal)
    {
        var upVect = Vector3.Cross( direction, normal);
        var tang = Vector3.Cross(normal, upVect);
        var newDirection = (direction + 
            tang * Random.Range(-Mathf.Sin(Mathf.Deg2Rad * 15), Mathf.Sin(Mathf.Deg2Rad * 15))).normalized;
        
        return newDirection;
    }
}
