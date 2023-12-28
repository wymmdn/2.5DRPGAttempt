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
        damageType = changeHealthType.physicDamage;
        attackInterval = attackIntervalInit = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void playAttack()
    {
        attackDir = new Vector2(Mathf.RoundToInt(attackDir.x), Mathf.RoundToInt(attackDir.y));
        base.playAttack();
    }

}
