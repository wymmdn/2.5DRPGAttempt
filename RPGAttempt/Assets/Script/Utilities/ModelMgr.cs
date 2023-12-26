using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GloblePath {
    public const string weaponPath = "Prefabs/weapon/";
    public const string defaultWeaponPath = weaponPath + "freeHand";
    public const string fireWand = weaponPath + "fireWand";
    public const string fireWandProjectile = weaponPath + "fireWand_Projectile";
}
public class tagtag {
    public const string player = "Player";
    public const string enemy = "Enemy";
    public const string npc = "NPC";
    public const string createdMap = "CreatedMap";
}

public class AnimtorParam {
    public static readonly int InputX   = Animator.StringToHash("InputX");
    public static readonly int InputY   = Animator.StringToHash("InputY");
    public static readonly int dirX     = Animator.StringToHash("dirX");
    public static readonly int dirY     = Animator.StringToHash("dirY");
    public static readonly int isMoving = Animator.StringToHash("isMoving");
    public static readonly int attack   = Animator.StringToHash("attack");
    public static readonly int getHurt  = Animator.StringToHash("getHurt");
    public static readonly int getHeal  = Animator.StringToHash("getHeal");
}
public enum changeHealthType
{ 
    physicDamage = 1,
    heal = 2,
    realDamage = 3,
    realHeal = 4,
    fireDamage = 5
}

public enum stateType
{
    idle = 1,
    chase = 2,
    attack = 3
}

namespace ModelMgr { }