using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssailable
{
    public void changeHealth(int health,changeHealthType type);
    public void toDead();
}
