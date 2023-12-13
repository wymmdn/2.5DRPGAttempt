using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    //Transform[] childs;
    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).rotation = Camera.main.transform.rotation;
        }
    }
}
