using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Biome Classes")]
    [SerializeField] private BiomeClass[] biomes;

    [Header("Generation Settings")]
    [SerializeField] private int worldWidth;
    [SerializeField] private int worldHeight;
    [SerializeField] private float noiseFreq, seed;
    [SerializeField] private Texture2D noiseTexture;

    [Header("Biomes")]
    [SerializeField] private float biomeFreq;
    [SerializeField] private Texture2D biomeMap;
    [SerializeField] private Gradient biomeGradient;

    [Header("Tilemap Settings")]
    [SerializeField] private Tilemap grassTileMap;
    [SerializeField] private Tilemap sandTileMap;
    [SerializeField] private Tilemap waterTileMap;
    [SerializeField] private Tile[] grass, sand, water;

    private BiomeClass currentBiome;

    private void OnValidate()
    {
        DrawTextures();
    }

    void Start()
    {
        seed = Random.Range(-100000, 100000);
        DrawTextures();
        GenerateTerrain();
    }
    private void DrawTextures()
    {
        GenerateNoiseTexture();
        biomeMap = new Texture2D(worldWidth, worldHeight);
        GenerateBiomeTexture();
    }
    private void GenerateTerrain()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                currentBiome = GetCurrentBiome(x, y);
                SetTileOfType(currentBiome.tileMap, new Vector3Int(x, y), currentBiome.groundTiles);
            }
        }
    }
    private void SetTileOfType(Tilemap tilemap, Vector3Int pos, Tile[] tiles)
    {
        tilemap.SetTile(pos, tiles[Random.Range(0, tiles.Length - 1)]);
    }
    private void GenerateBiomeTexture()
    {
        for (int x = 0; x < biomeMap.width; x++)
        {
            for (int y = 0; y < biomeMap.height; y++)
            {
                float perlin = Mathf.PerlinNoise((x + seed) * biomeFreq, (y + seed) * biomeFreq);
                Color c = biomeGradient.Evaluate(perlin);
                biomeMap.SetPixel(x, y, c);
            }
        }
        biomeMap.Apply();
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
    private BiomeClass GetCurrentBiome(int x, int y)
    {
        // Change currentBiome
        // Search through biomes
        for (int i = 0; i < biomes.Length; i++)
        {
            if (biomes[i].biomeCol == biomeMap.GetPixel(x, y))
            {
                return biomes[i];
            }
        }

        return currentBiome;
    }
}
