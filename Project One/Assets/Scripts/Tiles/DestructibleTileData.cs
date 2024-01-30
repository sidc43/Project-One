using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class DestructibleTileData : ScriptableObject
{
    public TileBase tile;
    public const int maxHealth = 100;
    public Item item;
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