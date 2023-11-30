using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelMgr;

public class HealthController : MonoBehaviour
{
    //private List<GameObject> hearts;
    private PlayerController player ;
    public GameObject heart;

    private void Awake()
    {
        //hearts = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
        healthDisplay(player.curHeart);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            if (this.transform.childCount > 0)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
        }*/
    }
    public void healthDisplay(int health)
    {
        Transform thistf = this.transform;
        if (health == thistf.childCount)
        {
            Debug.Log("called" + thistf.childCount + " - " + health);
            return;
        } 
        
        while (health > thistf.childCount)
        {
            Instantiate(heart,thistf);
        }
        while (health < thistf.childCount)
        {
            if (thistf.childCount > 0)
            {
                Transform childtf = thistf.GetChild(0);
                childtf.SetParent(null);
                Destroy(childtf.gameObject);
            }
        }
    }

}
