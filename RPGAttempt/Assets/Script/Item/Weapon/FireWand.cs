using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Weapon
{
    private List<FireWandProjectile> projectiles = new List<FireWandProjectile>();
    [SerializeField]private GameObject projectile;
    [SerializeField]private Transform firePoint;
    
    protected override void Awake()
    {
        base.Awake();
        positionOffset += new Vector3(0, 0.1f, 0);
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        go.GetComponent<Rigidbody2D>().velocity = attackDir * go.GetComponent<Projectile>().launchForce;
    }
}
