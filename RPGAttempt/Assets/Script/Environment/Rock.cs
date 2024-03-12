using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IAssailable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int curHealth;
    [SerializeField] private bool isInvicible;

    private void Awake()
    {
        
    }
    public void changeHealth(int health, changeHealthType type)
    {
        switch (type)
        {
            case changeHealthType.realDamage:
                if (!isInvicible)
                {
                    curHealth -= health;
                }
                break;
            default:
                if (!isInvicible)
                {
                    
                }
                break;
        }
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < 0)
        {
            curHealth = 0;
        }
    }

    public void toDead()
    {
        transform.SetParent(null);
        transform.position = new Vector3(999f, 999f, 999f);
        Destroy(this.gameObject);
    }
    private void Update()
    {
        if (curHealth <= 0)
        {
            toDead();
        }
    }
}
