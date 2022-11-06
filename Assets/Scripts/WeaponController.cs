using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : CharMovement
{
    protected string horizAxisName2 = "Mouse X";
    protected string vertAxisName2 = "Mouse Y";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        horizAxisName = horizAxisName2;
        vertAxisName = vertAxisName2;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
