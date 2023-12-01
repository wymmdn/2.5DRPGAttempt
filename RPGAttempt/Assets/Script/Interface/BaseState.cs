using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    //protected Role currentRole;
    //public abstract void OnEnter(Role role);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
