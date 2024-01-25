using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GloblePath {
    public const string weaponPath = "Prefabs/weapon/";
    public const string defaultWeaponPath = weaponPath + "freeHand";
    public const string fireWand = weaponPath + "fireWand";
    public const string fireWandProjectile = weaponPath + "fireWand_Projectile";
    public const string fireHand = weaponPath + "fireHand";
    public const string fireBoom = weaponPath + "fireBoom";
}
public class tagtag {
    public const string player = "Player";
    public const string enemy = "Enemy";
    public const string npc = "NPC";
    public const string createdMap = "CreatedMap";
    public const string ground = "Ground";
}
public class LayerString { 
    //public const string
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
    chase1 = 3,
    chase2 = 4,
    secondAttack = 5,
    attack = 6,
    run = 7
}
public enum equipmentName
{ 
    weapon = 1,
    headArmor = 2,
    bodyArmor = 3
}
public class UIString
{
    public const string pickUp = "ʰȡ";
    public const string discard = "����";
    public const string use = "ʹ��";
    public const string equip = "װ��";
    public const string unEquip = "����";
    public const string place = "����";
}

public class roleName
{
    public const string orangeMeow = "OrangeMeow";
    public const string treeMonster = "TreeMonster";
    public const string fireBear = "FireBear";
}
public class storyPlot
{
    public const string gotMission = "gotMission1";
    public const string completeMission = "completeMission1";
    public const string gotMission2 = "gotMission2";
    public const string completeMission2 = "completeMission2";
}
