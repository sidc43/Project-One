using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]

public class Item : ScriptableObject {
    public string itemName;
    public Sprite sprite;
    public int maxStack;
    public bool stackable;
    public int craftingResAmount;

    [SerializedDictionary("Item", "Amount")]
    public Dictionary<Item, int> itemsReq;

    public enum Rarity {
        Common,
        Rare,
        Epic,
        Unseen
    }
}