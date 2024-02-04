using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Diagnostics;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile borderTile;
    [SerializeField] private int worldWidth, worldHeight;
    [SerializeField] private int borderThickness;
    [SerializeField] private TerrainGeneration terrainGenerator;
    [SerializeField] public Light2D globalLight;
    private Stopwatch stopwatch;
    private static int noon = 10000;
    private static int midnight = 20000;
    private static float minIntensity = 0.1F;
    private static float maxIntensity = 1.0F;

    private void Start()
    {
        GenerateWorldBorders();
        worldHeight = terrainGenerator.GetWorldHeight;
        worldWidth = terrainGenerator.GetWorldWidth;

        globalLight.intensity = 0.5F;
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    void Update()
    {
        ToggleInventory();

        if (inventory.activeSelf)
            PauseGame();
        else
            ResumeGame();

        DayNightCycle();
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

    private void DayNightCycle()
    {
        TimeSpan ts = stopwatch.Elapsed;
        if (ts.Milliseconds == midnight)
        {
            stopwatch.Restart();
        } else if (ts.Milliseconds >= 0 && ts.Milliseconds < noon)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, maxIntensity, Time.deltaTime);
        } else if (ts.Milliseconds >= noon)
        {
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, minIntensity, Time.deltaTime);
        }
        
    }
}
