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

    private BiomeClass currentBiome;

    void Start()
    {
        seed = Random.Range(-100000, 100000);
        DrawTextures();
        GenerateTerrain();
    }
    private void DrawTextures()
    {
        // Create biome map and generate world noise texture
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
                if (currentBiome.biomeName == "Water")
                {
                    SetTileOfType(currentBiome.tileMap, new Vector3Int(x, y), currentBiome.animatedTile);
                }
                else
                { 
                    SetTileOfType(currentBiome.tileMap, new Vector3Int(x, y), currentBiome.groundTiles);
                }

                // Add foliage
                float treeChance = Random.Range(0f, 1f);
                float foliageChance = Random.Range(0f, 1f);
                float rockChance = Random.Range(0f, 1f);
                if (treeChance <= currentBiome.treeChance)
                {
                    SetTileOfType(currentBiome.treeTileMap, new Vector3Int(x, y), currentBiome.treeTiles);
                }
                if (foliageChance <= currentBiome.foliageChance)
                {
                    SetTileOfType(currentBiome.foliageTileMap, new Vector3Int(x, y), currentBiome.foliageTiles);
                }
                if (rockChance <= currentBiome.rockChance)
                {
                    SetTileOfType(currentBiome.rockTileMap, new Vector3Int(x, y), currentBiome.rockTiles);
                }
            }
        }
    }
    private void SetTileOfType(Tilemap tilemap, Vector3Int pos, Tile[] tiles)
    {
        tilemap.SetTile(pos, tiles[Random.Range(0, tiles.Length - 1)]);
    }
    private void SetTileOfType(Tilemap tilemap, Vector3Int pos, AnimatedTile tile)
    {
        tilemap.SetTile(pos, tile);
    }
    private void GenerateBiomeTexture()
    {
        for (int x = 0; x < biomeMap.width; x++)
        {
            for (int y = 0; y < biomeMap.height; y++)
            {
                // Generate perlin noise texture and set the color to the corresponding biome
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
    public int GetWorldWidth => worldWidth;
    public int GetWorldHeight => worldHeight;
}
