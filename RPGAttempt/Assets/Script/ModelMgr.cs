using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelMgr 
{
    public class Model { 
        
    }
    public class Path {
        public const string weaponPath = "Prefabs/weapon/";
        public const string defaultWeaponPath = weaponPath + "freeHand";
    }
    public class tagtag {
        public const string player = "Player";
        public const string enemy = "Enemy";
        public const string npc = "NPC";
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