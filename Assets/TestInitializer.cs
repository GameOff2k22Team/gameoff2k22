using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitializer : MonoBehaviour
{
    [SerializeField]
    NewBouncerSplitEnemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy.Initialize(new Vector3(.66f, .66f, 0), 2f, 1, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
