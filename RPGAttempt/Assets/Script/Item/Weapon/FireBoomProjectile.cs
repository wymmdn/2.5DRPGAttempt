using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireBoomProjectile : Projectile
{
    [SerializeField] private Tilemap createdMap;
    [SerializeField] private Tile fireTile;
    [SerializeField] private GameObject fireTilePrefab;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject target;
    protected override void Awake()
    {
        base.Awake();
        createdMap = GameObject.FindWithTag(tagtag.createdMap).GetComponent<Tilemap>();
        grid = createdMap.GetComponentInParent<Grid>();
    }

    protected override void Update()
    {
        base.Update();
        if(target != null)
            rb.velocity = ((Vector2)target.transform.position - (Vector2)transform.position).normalized * launchForce;
        else
            rb.velocity = weaponFrom.attackDir * launchForce;
    }
    public void launch(GameObject go)
    {
        target = go;
    }
    public override void explode()
    {
        Vector3Int point = grid.WorldToCell(transform.position);
        Tile tile = Instantiate(fireTile);
        tile.gameObject = fireTilePrefab;
        createdMap.SetTile(point, tile);
        createdMap.SetTile(new Vector3Int(point.x - 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x + 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y - 1, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y + 1, 0), tile);
        transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
