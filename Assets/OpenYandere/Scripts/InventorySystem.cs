using OpenYandere.Managers.Traits;
using System.Collections.Generic;
using UnityEngine;

internal class InventorySystem : Singleton<InventorySystem>
{
    public List<ItemBase> items = new();

    public bool AddItem(ItemBase itemToAdd)
    {
        // Logic to add item to the inventory (considering max capacity, stackability, etc.)
        items.Add(itemToAdd);
        return true;  // Return true if successfully added, false otherwise.
    }

    public bool RemoveItem(ItemBase itemToRemove)
    {
        // Logic to remove item from the inventory.
        return items.Remove(itemToRemove);
    }

    public bool Contains(ItemBase item)
    {
        return items.Contains(item);
    }
}
