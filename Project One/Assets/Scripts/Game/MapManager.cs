using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap[] groundTilemaps;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private List<GroundTileData> groundTileDatas;
    [SerializeField] private List<DestructibleTileData> destructibleTileDatas;
    [SerializeField] private List<InteractiveTileData> interactiveTileDatas;

    private Dictionary<TileBase, GroundTileData> dataFromGroundTiles;    
    private Dictionary<TileBase, DestructibleTileData> dataFromDestructibleTiles;    
    private Dictionary<TileBase, InteractiveTileData> dataFromInteractiveTiles;

    private void Start()
    {
        dataFromGroundTiles = new Dictionary<TileBase, GroundTileData>();
        dataFromDestructibleTiles = new Dictionary<TileBase, DestructibleTileData>();
        dataFromInteractiveTiles = new Dictionary<TileBase, InteractiveTileData>();

        InitializeTileDatas();
    }

    private void Update()
    {
        player.speed = GetGroundTileSpeed(player.transform.position);

        if (Utility.LMB())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // TODO: Loop through interactive and destructive tilemaps and take action accordingly
            foreach (var tilemap in groundTilemaps)
            {
                Vector3Int gridPos = tilemap.WorldToCell(mousePos);
                TileBase t = tilemap.GetTile(gridPos);
            }
        }
    }

    private void InitializeTileDatas()
    {
        // Feed data to all ground tiles
        foreach (var gTileData in groundTileDatas)
        {
            foreach (var tile in gTileData.tiles)
            {
                dataFromGroundTiles.Add(tile, gTileData);
            }
        }

        // Feed data to all destructible tiles
        foreach (var dTileData in destructibleTileDatas)
        {
            foreach (var tile in dTileData.tiles)
            {
                dataFromDestructibleTiles.Add(tile, dTileData);
            }
        }

        // Feed data to all interactive tiles
        foreach (var iTileData in interactiveTileDatas)
        {
            foreach (var tile in iTileData.tiles)
            {
                dataFromInteractiveTiles.Add(tile, iTileData);
            }
        }
    }
    public float GetGroundTileSpeed(Vector2 pos)
    {
        float walkSp = player.GetMaxSpeed();
        foreach (Tilemap tilemap in groundTilemaps)
        {
            Vector3Int gridPos = tilemap.WorldToCell(pos);
            TileBase tile = tilemap.GetTile(gridPos);
            if (tile != null)
            {
               walkSp = dataFromGroundTiles[tile].walkingSpeed;
            }
            return walkSp;
        }
        return walkSp;
    }
}
