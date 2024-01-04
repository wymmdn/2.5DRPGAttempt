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
    }
    private void OnEnable()
    {
        EventHandler.CloseInteractPanel += performAct;
    }
    private void OnDisable()
    {
        EventHandler.CloseInteractPanel -= performAct;
    }
    private void Update()
    {
        if (curChild != noneChild && Input.GetMouseButtonUp(1))
        {
            rightClick();
        }
    }
    private void performAct(string act)
    {
        if (clickChild == noneChild) return;
        switch (act)
        {
            case "":
                break;
            case UIString.unEquip:
                unEquip((int)clickChild);
                break;
            default:
                break;
        }
        clickChild = noneChild;
    }
    private void unEquip(int equipName)
    {
        if (equipName == (int)equipmentName.weapon)
        {
            weapon.isPickable = true;
            Weapon def = Instantiate(weapon.master.defaultWeapon).GetComponent<Weapon>();
            def.equip(weapon.master);
            weapon = null;
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
    public void displayEquipments(PlayerController player)
    { 
        
    }
    public void rightClick()
    {
        
        switch (curChild)
        { 
            case noneChild:
                break;
            case equipmentName.weapon:
                if (this.weapon != null)
                { 
                    clickChild = curChild;
                    EventHandler.CallOpenInteractPanel(interactOptions,this.weapon,weaponIcon.transform.position);
                }
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
