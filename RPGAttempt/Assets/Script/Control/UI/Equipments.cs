using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipments : MonoBehaviour
{
    [SerializeField] private GameObject weapon; 
    public void displayWeapon(Weapon wp)
    {
        SpriteRenderer sp = wp.GetComponent<SpriteRenderer>() != null ? wp.GetComponent<SpriteRenderer>(): wp.GetComponentInChildren<SpriteRenderer>();
        var tmpColor = weapon.GetComponent<Image>().color;
        if (sp != null)
        {
            tmpColor.a = 1f;
            weapon.GetComponent<Image>().sprite = sp.sprite;
        }
        else {
            tmpColor.a = 0f;
        }
        weapon.GetComponent<Image>().color = tmpColor;
    }
    public void displayHeadArmor()
    { 
    
    }
    public void displayBodyArmor()
    { 
    
    }
    public void displayEquipments(PlayerController player)
    { 
        
    }
}
