using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Weapon weaponFrom;
    [SerializeField]public float survivalTime;
    [SerializeField]public float launchForce;
    private float timeCnt;
    protected virtual void Awake()
    {
        weaponFrom = GetComponentInParent<Weapon>();
        timeCnt = survivalTime;
    }
    protected virtual void Update()
    {
        timeCnt -= Time.deltaTime;
        if (timeCnt < 0)
        { 
            explode();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IAssailable>() != null && collision.tag != weaponFrom.master.tag)
        {
            collision.GetComponent<IAssailable>().changeHealth(weaponFrom.damage, weaponFrom.damageType);
            explode();
        }
    }
    public virtual void explode()
    {
        transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
