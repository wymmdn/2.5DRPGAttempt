using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class TrapMgr : MonoBehaviour
{
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.changeHealth(damage, changeHealthType.physicDamage);
        }
    }
}
