using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatedGround : MonoBehaviour
{
    private Tilemap createdMap;
    private Grid grid;
    private int fireDamage;
    private float fireEffectTime;
    private float fireEffectTimeCnt;
    private void Awake()
    {
        createdMap = GetComponent<Tilemap>();
        grid = GetComponentInParent<Grid>();
        fireEffectTime = 1f;
        fireEffectTimeCnt = 1f;
        fireDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        fireEffectTimeCnt += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.GetComponent<IAssailable>() == null) 
            return;
        Tile tile = createdMap.GetTile<Tile>(grid.WorldToCell(collision.transform.position));
        if (tile == null) 
            return;
        if (tile.gameObject.GetComponent<GroundTile>() is FireGroundTile)
        {
            if (fireEffectTimeCnt > fireEffectTime)
            {
                collision.GetComponent<IAssailable>().changeHealth(fireDamage, changeHealthType.fireDamage);
                fireEffectTimeCnt = 0f;
            }
        }
    }
}
