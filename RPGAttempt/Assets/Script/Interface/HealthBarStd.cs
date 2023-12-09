using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarStd : MonoBehaviour
{
    private Vector3 maxScale;
    private Vector3 originScale;

    private void Awake()
    {
        maxScale = this.transform.GetChild(0).GetComponent<RectTransform>().localScale;
        originScale = this.transform.localScale;
        this.transform.localScale = Vector3.zero;
    }
    public void healthDisplay(float healthPercent)
    {
        this.transform.localScale = originScale;
        this.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(healthPercent * maxScale.x, maxScale.y, maxScale.z);
    }
}
