using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonsterBody : Item
{
    [SerializeField] private FireBear fireBear;

    private void OnEnable()
    {
        EventHandler.FireBearCame += genFireBear;
    }
    private void OnDisable()
    {
        EventHandler.FireBearCame -= genFireBear;
    }
    public FireBear genFireBear()
    { 
        return Instantiate(fireBear,transform.position,new Quaternion(),transform.parent);
    }
}
