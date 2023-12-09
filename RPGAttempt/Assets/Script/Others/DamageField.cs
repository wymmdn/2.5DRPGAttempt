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
        Debug.Log("called");
        Role targetRole = collision.gameObject.GetComponent<Role>();
        if (targetRole != null && weaponFrom != null)
        {
            targetRole.changeHealth(damage, damageType);
        }
    }



}
