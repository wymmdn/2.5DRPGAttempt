using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireWandProjectile : Projectile
{
    [SerializeField] private Tilemap createdMap;
    [SerializeField] private TileBase fireTile;
    [SerializeField] private Grid grid;
    protected override void Awake()
    {
        base.Awake();
        createdMap = GameObject.FindWithTag(tagtag.createdMap).GetComponent<Tilemap>();
        grid = createdMap.GetComponentInParent<Grid>();
    }
    public override void explode()
    {
        Vector3Int point = grid.WorldToCell(transform.position);
        TileBase tile = Instantiate(fireTile);
        createdMap.SetTile(point, tile);
        createdMap.SetTile(new Vector3Int(point.x - 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x + 1, point.y, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y - 1, 0), tile);
        createdMap.SetTile(new Vector3Int(point.x, point.y + 1, 0), tile);
        transform.SetParent(null);
        Destroy(this.gameObject);
    }
}
