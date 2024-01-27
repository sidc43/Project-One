using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GroundTileData : ScriptableObject
{
    public TileBase[] tiles;
    public float walkingSpeed;

    public bool harmful;
    [Header("If harmful")]
    public float damage;


}
