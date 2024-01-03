using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipments : MonoBehaviour
{
    [SerializeField] private GameObject weaponIcon;
    [SerializeField] private Weapon weapon;
    [SerializeField] private InteractPanel interactPanel; // init value in inspector
    [SerializeField] private equipmentName curChild = noneChild;
    [SerializeField] private equipmentName clickChild;
    private const int noneChild = 0;
    private List<string> interactOptions = new List<string>();

    private void Awake()
    {
        interactOptions.Add(UIString.unEquip);
        interactOptions.Add(UIString.place);
        interactOptions.Add(UIString.discard);
    }
    private void Update()
    {
        if (curChild != noneChild && Input.GetMouseButtonUp(1))
        {
            rightClick();
        }
    }
    public void displayWeapon(Weapon wp)
    {
        SpriteRenderer sp = wp.GetComponent<SpriteRenderer>() != null ? wp.GetComponent<SpriteRenderer>(): wp.GetComponentInChildren<SpriteRenderer>();
        var tmpColor = weaponIcon.GetComponent<Image>().color;
        if (sp != null)
        {
            tmpColor.a = 1f;
            weaponIcon.GetComponent<Image>().sprite = sp.sprite;
            this.weapon = wp;
        }
        else {
            tmpColor.a = 0f;
        }
        weaponIcon.GetComponent<Image>().color = tmpColor;
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
    public void rightClick()
    {
        clickChild = curChild;
        switch (clickChild)
        { 
            case noneChild:
                break;
            case equipmentName.weapon:
                if (this.weapon != null)
                    EventHandler.CallOpenInteractPanel(interactOptions,this.weapon,weaponIcon.transform.position);
                break;
            case equipmentName.headArmor:
            case equipmentName.bodyArmor:
                break;
            default:
                break;
        }
    }
    public void enterWeapon()
    {
        curChild = equipmentName.weapon;
    }
    public void enterHeadArmor()
    {
        curChild = equipmentName.headArmor;
    }
    public void enterBodyArmor()
    {
        curChild = equipmentName.bodyArmor;
    }
    public void exitChild()
    {
        curChild = noneChild;
    }
}
