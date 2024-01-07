using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeHand : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        this.isPickable = false;
    }
    protected override void playAttack()
    {
        attackDir = new Vector2(Mathf.RoundToInt(attackDir.x), Mathf.RoundToInt(attackDir.y));
        base.playAttack();
    }
    public override void unEquip()
    {
        this.transform.SetParent(null);
        Destroy(this.gameObject);
    }

}
