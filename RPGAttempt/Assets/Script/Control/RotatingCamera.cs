using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private bool isRotating = false;
    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == sceneName.magicValley)
        { 
            Rotate();
        }
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            StartCoroutine(RotateAround(-90, rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            StartCoroutine(RotateAround(90, rotateTime));
        }
    }

    IEnumerator RotateAround(float angel, float time)
    {
        float number = 60 * time;
        float nextAngel = angel / number;
        isRotating = true;

        for (int i = 0; i < number; i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngel));
            yield return new WaitForFixedUpdate();
        }

        isRotating = false;
    }
}
