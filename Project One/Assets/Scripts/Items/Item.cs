using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]

public class Item : ScriptableObject {
    [Header("Details")]
    public string itemName;
    public Sprite sprite;

    [Header("Attributes")]
    public int maxStack;
    public bool stackable;

    public enum Rarity 
    {
        Common,
        Rare,
        Epic,
        Unseen
    }
}