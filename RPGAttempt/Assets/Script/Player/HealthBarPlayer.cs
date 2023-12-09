using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class HealthBarPlayer : MonoBehaviour
{
    //private List<GameObject> hearts;
    public GameObject heart;

    public void healthDisplay(int health)
    {
        Transform thistf = this.transform;
        if (health == thistf.childCount)
        {
            return;
        } 
        
        while (health > thistf.childCount)
        {
            Instantiate(heart,thistf);
        }
        while (health < thistf.childCount)
        {
            if (thistf.childCount > 0)
            {
                Transform childtf = thistf.GetChild(0);
                childtf.SetParent(null);
                Destroy(childtf.gameObject);
            }
        }
    }

}
