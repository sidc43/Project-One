using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Tilemap> groundTilemaps;
    [SerializeField] private List<Tilemap> destructibleTilemaps;
    [SerializeField] private Tilemap playerPlacedTilemap;
    [SerializeField] private PlayerMovement player;
    private Vector3Int breakingBlock = Vector3Int.zero;

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
        HandleDestroyBlock();
        HandlePlaceBlock();
        
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
            dataFromDestructibleTiles.Add(dTileData.tile, dTileData);
        }

        // Feed data to all interactive tiles
        foreach (var iTileData in interactiveTileDatas)
        {
            dataFromInteractiveTiles.Add(iTileData.tile, iTileData);
        }
    }
    public float GetGroundTileSpeed(Vector2 pos)
    {
        for (int i = 0; i < groundTilemaps.Count; i++)
        {
            // TODO: Fix to foreach
            Vector3Int gridPos = groundTilemaps[i].WorldToCell(pos);
            TileBase tile = groundTilemaps[i].GetTile(gridPos);
            if (tile != null)
            {   
                return dataFromGroundTiles[tile].walkingSpeed;
            }
        }
        return player.GetMaxSpeed();
    }

    public void HandleDestroyBlock()
    {
        if (Utility.LMB())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < destructibleTilemaps.Count; i++)
            {
                TileBase t;
                Vector3Int gridPos = destructibleTilemaps[i].WorldToCell(mousePos);
                if (!gridPos.Equals(breakingBlock))
                {   
                    t = destructibleTilemaps[i].GetTile(gridPos);
                    dataFromDestructibleTiles[t].SetHealthMax();
                    breakingBlock = gridPos;
                }
                t = destructibleTilemaps[i].GetTile(gridPos);
                if (!dataFromDestructibleTiles[t].ReduceHealth(10))
                {
                    destructibleTilemaps[i].SetTile(gridPos, null);
                }
            }

            // TODO: Loop through player placed tilemap and do the same
        }
    }

    public void HandlePlaceBlock()
    {
        if (Utility.RMB())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < groundTilemaps.Count; i++)
            {
                Vector3Int gridPos = groundTilemaps[i].WorldToCell(mousePos);
                
                //playerPlacedTilemap.SetTile(gridPos, wood);
            }
        }
    }
}