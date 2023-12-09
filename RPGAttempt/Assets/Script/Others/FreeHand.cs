using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class FreeHand : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        damage = 1;
        damageType = changeHealthType.damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
