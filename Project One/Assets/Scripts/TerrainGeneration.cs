using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHeight;
    [SerializeField] private float noiseFreq, seed;
    [SerializeField] private Texture2D noiseTexture;

    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap grassTileMap;
    [SerializeField] private Tilemap sandTileMap;
    [SerializeField] private Tilemap waterTileMap;
    [SerializeField] private Tile[] grass, sand, water;

    void Start()
    {
        seed = Random.Range(-100000, 100000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                if (noiseTexture.GetPixel(x, y).r < 0.5f)
                    grassTileMap.SetTile(new Vector3Int(x, y), grass[Random.Range(0, grass.Length - 1)]);
                else
                    waterTileMap.SetTile(new Vector3Int(x, y), water[Random.Range(0, water.Length - 1)]);
            }
        }
    }

    private void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldWidth, worldHeight);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float perlin = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(perlin, perlin, perlin));
            }
        }
        noiseTexture.Apply();
    }
}
