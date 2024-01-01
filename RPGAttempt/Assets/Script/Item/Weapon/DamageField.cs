using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        IAssailable target = collision.gameObject.GetComponent<IAssailable>();
        if (target != null && weaponFrom != null)
        {
            target.changeHealth(damage, damageType);
        }
    }



}
