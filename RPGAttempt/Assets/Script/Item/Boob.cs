using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Boob : Item , IAssailable
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int curHealth;
    [SerializeField] protected bool isInvicible;
    [SerializeField] protected float invicibleTime;
    [SerializeField] protected float physicResist;
    [SerializeField] protected float fireResist;

    [SerializeField] private Tilemap createdMap;
    [SerializeField] private Tile fireTile;
    [SerializeField] private GameObject fireTilePrefab;
    [SerializeField] private Grid grid;

    [SerializeField] private float explodeRadius;

    protected override void Awake()
    {
        base.Awake();
        createdMap = GameObject.FindWithTag(tagtag.createdMap).GetComponent<Tilemap>();
        grid = createdMap.GetComponentInParent<Grid>();
        explodeRadius = 0.5f;
    }
    public void changeHealth(int health, changeHealthType type)
    {
        switch (type)
        {
            case changeHealthType.heal:
                curHealth += health;
                break;
            case changeHealthType.physicDamage:
                if (!isInvicible)
                {
                    dealPhysicDamage(health);
                }
                break;
            case changeHealthType.fireDamage:
                if (!isInvicible)
                {
                    dealFireDamage(health);
                }
                break;
            default:
                if (!isInvicible)
                {
                    curHealth -= health;
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
    { }
    public void explode()
    {
        //hard code
        var player = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
        if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) < explodeRadius)
        {
            player.changeHealth(1, changeHealthType.physicDamage);
        }


        Vector3Int point = grid.WorldToCell(transform.position);
        Tile tile = Instantiate(fireTile);
        tile.gameObject = fireTilePrefab;
        createdMap.SetTile(point, tile);
        createdMap.SetTile(new Vector3Int(point.x - 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x + 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y - 1, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y + 1, 0), tile);
        transform.SetParent(null);
        transform.position = new Vector3(999f, 999f, 999f);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (curHealth <= 0)
        { 
            explode();
        }
    }
    protected virtual void dealPhysicDamage(int health)
    {
        health = (int)(health * (1 - physicResist));
        curHealth -= health;
    }
    protected virtual void dealFireDamage(int health)
    {
        health = (int)(health * (1 - fireResist));
        curHealth -= health;
    }
}