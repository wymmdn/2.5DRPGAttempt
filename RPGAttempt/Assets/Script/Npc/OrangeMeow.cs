using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMeow : NPC
{

    private void OnEnable()
    {
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
    }

}
