using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class BiomeClass
{
    [Header("General Settings")]
    public string biomeName;
    public Color biomeCol;

    [Header("Generation Settings")]
    public Tilemap tileMap;
    public Tile[] groundTiles, treeTiles, foliageTiles, rockTiles;

    [Header("Foliage settings")]
    [Range(0, 1)]
    public float treeChance;
    [Range(0, 1)]
    public float bushChance;
    [Range(0, 1)]
    public float rockChance;
}
