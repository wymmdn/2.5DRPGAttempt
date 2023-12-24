using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class DamageField : MonoBehaviour
{
    private Weapon weaponFrom;
    private int damage { get; set; }
    private changeHealthType damageType { get; set; }
    private void Start()  //�ݶ�damage ��damagetype����weaponԤ��д�����Ż�������damagefieldӦ����weapon��̬���������⣺���ʹ��instantiate�����Ѷ�̬����polygon����״��
    {
        weaponFrom = transform.parent.GetComponent<Weapon>(); 
        damage = weaponFrom.damage;
        damageType = weaponFrom.damageType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Role targetRole = collision.gameObject.GetComponent<Role>();
        Debug.Log(collision.gameObject.name);
        Debug.Log(weaponFrom.gameObject.name);
        if (targetRole != null && weaponFrom != null)
        {
            Debug.Log("field");
            targetRole.changeHealth(damage, damageType);
        }
    }



}
