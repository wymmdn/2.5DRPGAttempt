using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireGroundTile : GroundTile
{
    private Tile tile;
    private Vector3Int tilePosition;
    private Tilemap createdMap;
    private Grid grid;
    private float survivalTime;
    private void Awake()
    {
        
    }
    private void Start()
    {
        createdMap = GetComponentInParent<Tilemap>();
        grid = createdMap.GetComponentInParent<Grid>();
        tilePosition = grid.LocalToCell(transform.position);
        tile = createdMap.GetTile(tilePosition) as Tile;
        survivalTime = 5f;
    }
    private void Update()
    {
        survivalTime -= Time.deltaTime;
        if (survivalTime < 0)
        {
            createdMap.SetTile(tilePosition, null);         
        }
    }
}
