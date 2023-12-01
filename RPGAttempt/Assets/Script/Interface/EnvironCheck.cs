using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironCheck : MonoBehaviour
{
    [Header("¼ì²â²ÎÊý")]
    public Vector3 bottomOffset;
    public Vector3 topOffset;
    public Vector3 leftOffset;
    public Vector3 rightOffset;
    public Vector3 centerOffset;
    public float checkRadius;
    public LayerMask checkLayer;

    [Header("×´Ì¬")]
    public bool bottomBarrier;
    public bool topBarrier;
    public bool leftBarrier;
    public bool rightBarrier;
    public bool onTrap;
    public bool onWater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    { 
        
    }
}
