using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenYandere.Characters.Player;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemBase : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] protected string itemName = "New Item";
    [SerializeField] protected string itemDescription = "Description here...";
    [SerializeField] protected Sprite itemIcon;
    [SerializeField] protected GameObject itemPrefab;
    [SerializeField] protected ItemType itemType;
    public enum ItemType
    {
        Helmet, Armor, Weapon, Boots, Accessory, Consumable
    }

    [Header("Attributes")]
    [SerializeField] private int itemValue = 0; // Valor monet�rio do item, por exemplo.
    [SerializeField, Range(1, 100)] private int itemRarity = 1; // Um valor que define a raridade. 1- comum, 100- ultra raro.
    [SerializeField] private bool isConsumable = false; // Se o item pode ser consumido ou n�o.

    // Propriedades p�blicas para os campos acima.
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemIcon => itemIcon;
    public GameObject ItemPrefab => itemPrefab;
    public ItemType Type => itemType;
    public int Value => itemValue;
    public int Rarity => itemRarity;
    public bool IsConsumable => isConsumable;

    // A��es padr�o quando o item � usado ou equipado.
    // Estes podem ser sobrescritos por classes derivadas para comportamento personalizado.
    public virtual void Use()
    {
        if (isConsumable)
        {
            // L�gica para itens consum�veis. Exemplo: restaurar HP.
        }
    }

    public virtual void Equip()
    {
        // L�gica para equipar o item, se aplic�vel.
    }
}