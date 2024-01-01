using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    private Weapon weaponFrom;
    private int damage { get; set; }
    private changeHealthType damageType { get; set; }
    private void Start()  //暂定damage 和damagetype都由weapon预先写死，优化方向是damagefield应该由weapon动态创建（问题：如果使用instantiate，很难动态设置polygon的形状）
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
