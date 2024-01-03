using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public bool isPickable { get; set; }
    public void pickUp(Role role);
    public void discard();
}
