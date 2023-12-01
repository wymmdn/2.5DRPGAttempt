using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : BaseState
{
    protected Enemy currentEnemy;

    public abstract void OnEnter(Enemy enemy);
}
