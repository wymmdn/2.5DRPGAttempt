using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBush : MonoBehaviour
{

    public bool triggered;
    private Collider2D bushCol;
    private Collider2D playerCol;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        bushCol = GetComponent<CircleCollider2D>();
        
        //去gamemanager注册bush
        GameManager.RegisterBush(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {   //最好是获得碰撞到bushcol的其他col，然后判断其他col中有没有tag是player的
            
            playerCol = GameObject.FindWithTag(tagtag.player).GetComponent<Collider2D>();
            if (Physics2D.IsTouching(bushCol, playerCol)) 
            {
                //Debug.Log("press t " + bushCol.gameObject.name);
                triggered = true;
                GameManager.TriggerBush();
            }
        }
    }

    /*public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("---" + collision.gameObject.tag);
        if (collision.gameObject.tag == tagtag.player && Input.GetKey(keyname.cheat))
        {
            triggered = true;
            GameManager.TriggerBush();
        }
    }*/
}
