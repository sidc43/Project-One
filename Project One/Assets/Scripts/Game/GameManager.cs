using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile borderTile;
    [SerializeField] private int worldWidth, worldHeight;
    [SerializeField] private int borderThickness;
    [SerializeField] private TerrainGeneration terrainGenerator;

    private void Start()
    {
        GenerateWorldBorders();
        worldHeight = terrainGenerator.GetWorldHeight;
        worldWidth = terrainGenerator.GetWorldWidth;
    }

    void Update()
    {
        ToggleInventory();

        if (inventory.activeSelf)
            PauseGame();
        else
            ResumeGame();
    }
    private void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && inventory.activeSelf) 
        {
            inventory.SetActive(false);
        }
    }
    private void PauseGame() => Time.timeScale = 0;
    private void ResumeGame() => Time.timeScale = 1;
    private void GenerateWorldBorders()
    {
        // Top border
        for (int x = -borderThickness; x < worldWidth + borderThickness; x++)
        {
            for (int y = worldHeight; y < worldHeight + borderThickness; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), borderTile);
            }
        }

        // Bottom border
        for (int x = -borderThickness; x < worldWidth + borderThickness; x++)
        {
            for (int y = -borderThickness - 1; y < 0; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), borderTile);
            }
        }

        // Left border
        for (int x = -borderThickness - 1; x < 0; x++)
        {
            for (int y = -borderThickness; y < worldHeight + borderThickness; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), borderTile);
            }
        }

        // Right border
        for (int x = worldWidth; x < worldWidth + borderThickness; x++)
        {
            for (int y = -borderThickness; y < worldHeight + borderThickness; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), borderTile);
            }
        }
    }
}
