using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireBearStates;

public class FireBear : Enemy
{
    private Weapon secondWeapon;
    private EnemyState chase1State;
    private EnemyState secondAttackState;
    private EnemyState chase2State;

    public float chaseRadius;
    //public float alertRadius;
    public float secondAttackRadius;
    public float closeRadius;
    //public float attackRadius;
    protected override void Awake()
    {
        base.Awake();
        idleState = new FireBearIdleState();
        chase1State = new FireBearChase1State();
        secondAttackState = new FireBearSecondAttackState();
        chase2State = new FireBearChase2State();
        attackState = new FireBearAttackState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase1, chase1State);
        states.Add(stateType.secondAttack, secondAttackState);
        states.Add(stateType.chase2, chase2State);
        states.Add(stateType.attack, attackState);

        //equip secondWeapon
        secondWeapon = Instantiate((GameObject)Resources.Load(GloblePath.fireBoom), transform).GetComponent<Weapon>();
        secondWeapon.master = this;
        secondWeapon.isPickable = false;
        secondWeapon.transform.position = this.transform.position + secondWeapon.positionOffset;

        player = GameObject.FindGameObjectWithTag(tagtag.player);
        chaseRadius = alertRadius + 1.0f;
        secondAttackRadius = secondWeapon.attackRadius;
        closeRadius = 1f;
    }
    protected override void UpdateContent()
    {
        base.UpdateContent();

    }
    public override void attack()
    {
        rb.velocity = Vector2.zero;
        weapon.attack();
    }
    public void secondAttack()
    {
        rb.velocity = Vector2.zero;
        secondWeapon.attack();
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position, alertRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, attackRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, chaseRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, secondAttackRadius);
    }
    /*public override void toDead()
    {
        base.toDead();
        //EventHandler.CallFireBear(StoryManager.dead);
    }*/
}
