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
        attackInterval = attackIntervalInit = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
