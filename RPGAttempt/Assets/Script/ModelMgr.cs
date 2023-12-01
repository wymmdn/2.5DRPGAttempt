using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelMgr 
{
    public class Model { 
        
    }
    public class tagtag {
        public static string player = "Player";
        public static string enemy = "Enemy";

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