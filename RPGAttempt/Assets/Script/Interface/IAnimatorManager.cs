using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatorManager
{
    void movingAnimation();
    void idleAnimation();
    void attackAnimation();
    void getHurtAnimation();
    void getHealAnimation();
}
