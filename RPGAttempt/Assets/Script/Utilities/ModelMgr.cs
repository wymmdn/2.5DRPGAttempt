using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelMgr 
{
    public class Model { 
        
    }
    public static class GloblePath {
        public const string weaponPath = "Prefabs/weapon/";
        public const string defaultWeaponPath = weaponPath + "freeHand";
    }
    public class tagtag {
        public const string player = "Player";
        public const string enemy = "Enemy";
        public const string npc = "NPC";
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
        damage = 1,
        heal = 2,
        realDamage = 3,
        realHeal = 4
    }

    public enum stateType
    {
        idle = 1,
        chase = 2,
        attack = 3
    }
}