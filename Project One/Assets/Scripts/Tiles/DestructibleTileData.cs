using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class DestructibleTileData : ScriptableObject
{
    public TileBase[] tiles;
    public const int maxHealth = 100;
    [SerializeField] private int health;

    public void OnEnable()
    {
        SetHealthMax();
    }

    public void SetHealthMax() 
    {
        health = maxHealth;
    }

    public bool ReduceHealth(int damage)
    {   
        health -= damage;
        return (health > 0);
    }

    public int GetHealth() => health;
}