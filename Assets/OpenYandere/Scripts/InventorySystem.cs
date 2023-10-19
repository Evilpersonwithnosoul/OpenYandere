using OpenYandere.Managers.Traits;
using System.Collections.Generic;
using UnityEngine;

internal class InventorySystem : Singleton<InventorySystem>
{
    [SerializeField] protected List<ItemBase> items = new();
    // Eventos para notificar sobre mudanças no inventário.
    public delegate void InventoryChange();
    public event InventoryChange OnItemAdded, OnItemRemoved;

    public bool AddItem(ItemBase itemToAdd)
    {
        // Logic to add item to the inventory (considering max capacity, stackability, etc.)
        items.Add(itemToAdd);
        OnItemAdded?.Invoke();  // Notificar sobre o item adicionado.
        return true;  // Return true if successfully added, false otherwise.
    }

    public bool RemoveItem(ItemBase itemToRemove)
    {
        bool removed = items.Remove(itemToRemove);
        if (removed)
        {
            OnItemRemoved?.Invoke();  // Notificar sobre o item removido.
        }
        return removed;

    }

    public bool Contains(ItemBase item)
    {
        return items.Contains(item);
    }
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items.RemoveAt(index);
            OnItemRemoved?.Invoke();  // Notificar sobre o item removido.
        }
    }
    public List<ItemBase> GetItems()
    {
        return items;
    }
    public ItemBase GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;  // Return null if the index is out of range.
    }
}
