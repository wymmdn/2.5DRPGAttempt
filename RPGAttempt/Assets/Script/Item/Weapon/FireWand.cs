using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Weapon
{
    private List<FireWandProjectile> projectiles = new List<FireWandProjectile>();
    [SerializeField]private GameObject projectile;
    [SerializeField]private Transform firePoint;
    [SerializeField]private float launchForce;
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
    protected override void playAttack()
    {
        transform.right = attackDir;
        master.animatorManager.attackAnimation(attackDir);
        GameObject go = Instantiate(projectile, firePoint.position, new Quaternion(), transform);
        go.GetComponent<Rigidbody2D>().velocity = attackDir * launchForce;
    }
}
