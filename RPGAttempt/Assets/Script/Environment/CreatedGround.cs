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

    private List<Transform> assailables = new List<Transform>();
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
        effect();
    }
    private void effect()
    {
        for (int i = assailables.Count - 1; i >= 0; i--)
        {
            if (!assailables[i].TryGetComponent<IAssailable>(out var assailable))
            {
                assailables.RemoveAt(i);
                continue;
            }
            if (fireEffectTimeCnt > fireEffectTime)
            {
                assailable.changeHealth(fireDamage, changeHealthType.fireDamage);
            }
        }
        if(fireEffectTimeCnt > fireEffectTime)
            fireEffectTimeCnt = 0f;
    }
    /*private void OnTriggerStay2D(Collider2D collision)
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
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        //IAssailable obj = other.GetComponent<IAssailable>();
        if (!assailables.Contains(other.transform))
        {
            assailables.Add(other.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (assailables.Contains(other.transform))
        {
            assailables.Remove(other.transform);
        }
    }
}
