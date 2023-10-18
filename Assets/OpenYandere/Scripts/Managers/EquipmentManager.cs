using OpenYandere.Managers.Traits;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace OpenYandere.Managers
{
    internal class EquipmentManager : Singleton<EquipmentManager>
    {
        [SerializeField] protected Transform helmetPosition, armorPosition, weaponPosition;
        // Add more transforms for other equipment types
        public int maxPoolSize=10;
        [SerializeField] protected ItemBase currentHelmet, currentArmor, currentWeapon;
        [SerializeField] protected ItemBase[] allItems;
        private Dictionary<ItemBase, ObjectPool<GameObject>> itemPools = new();

        private void Awake()
        {

            foreach (var item in allItems)
            {
                ObjectPool<GameObject> poolForItem = new(
                    createFunc: () => Instantiate(item.itemPrefab),
                    actionOnGet: (obj) => obj.SetActive(true),
                    actionOnRelease: (obj) => obj.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    defaultCapacity: 10
                );

                itemPools[item] = poolForItem;
            }
           // if (allItems.Length > 0)
            //{
            //    EquipItem(ref currentWeapon, allItems[0], weaponPosition);
           // }
           //PopupMessage.Instance.onDisplayMessage.AddListener()
        }


        public void Equip(ItemBase item)
        {
            // Check if the item is in the inventory.
            if (!InventorySystem.Instance.Contains(item))
            {
                Debug.LogWarning("Trying to equip an item that's not in the inventory!");
                return;
            }

            switch (item.itemType)
            {
                case ItemBase.ItemType.Helmet:
                    EquipItem(ref currentHelmet, item, helmetPosition);
                    break;
                case ItemBase.ItemType.Armor:
                    EquipItem(ref currentArmor, item, armorPosition);
                    break;
                case ItemBase.ItemType.Weapon:
                    EquipItem(ref currentWeapon, item, weaponPosition);
                    break;
            }
        }

        public void EquipItem(ref ItemBase currentItem, ItemBase newItem, Transform equipPosition)
        {
            //PopupMessage.Instance.onDisplayMessage.Invoke($"Item {newItem.itemName} has been equipped");
            if (PopupMessage.Instance != null)
            {
                PopupMessage.Instance.onDisplayMessage.Invoke("Visibly armed.");
            }
            else
            {
                Debug.LogWarning("PopupMessage instance is null.");
            }
            
            if (newItem.itemPrefab == null)
            {
                Debug.LogWarning($"Item {newItem.itemName} does not have an associated prefab.");
                return;
            }
            if (currentItem != null)
            {
                UnequipItem(equipPosition);
            }

            GameObject itemInstance = itemPools[newItem].Get();
            if (itemInstance == null)
            {
                Debug.LogWarning($"Could not retrieve an instance of {newItem.itemName} from the ObjectPool.");
                return;
            }

            itemInstance.transform.SetParent(equipPosition);
            itemInstance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            itemInstance.SetActive(true);

            currentItem = newItem;
        }

        public void Unequip(ItemBase item)
        {
            switch (item.itemType)
            {
                case ItemBase.ItemType.Helmet:
                    UnequipItem(helmetPosition);
                    currentHelmet = null;
                    break;
                case ItemBase.ItemType.Armor:
                    UnequipItem(armorPosition);
                    currentArmor = null;
                    break;
                case ItemBase.ItemType.Weapon:
                    UnequipItem(weaponPosition);
                    currentWeapon = null;
                    break;
            }
        }


        private void UnequipItem(Transform equipPosition)
        {
            // Loop backwards through all children to safely release them to the pool
            for (int i = equipPosition.childCount - 1; i >= 0; i--)
            {
                GameObject unequippedItem = equipPosition.GetChild(i).gameObject;
                unequippedItem.SetActive(false);

                // Determine which item this GameObject represents
                ItemBase correspondingItem = allItems.FirstOrDefault(item => item.itemPrefab == unequippedItem);

                if (correspondingItem != null && itemPools.ContainsKey(correspondingItem))
                {
                    Debug.Log("Unequipping the Object: " + unequippedItem.name);
                    itemPools[correspondingItem].Release(unequippedItem);
                }
                else
                {
                    Debug.LogWarning("Could not find the corresponding item for: " + unequippedItem.name);
                }
            }
        }



        public ItemBase GetWeapon()
        {
            return currentWeapon;
        }

        public Transform GetWeaponSlot()
        {
            return weaponPosition;
        }

    }

}
