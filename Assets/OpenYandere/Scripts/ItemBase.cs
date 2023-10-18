using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.Player;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public ItemType itemType;
    public int damageValue;

    public enum ItemType
    {
        Helmet, Armor, Weapon, Boots, Accessory, Consumable
    }
}