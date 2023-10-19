using UnityEngine;
using System;
using System.Collections.Generic;
using OpenYandere.Managers;

namespace OpenYandere.Characters.Player
{
    public class Player : Character
    {
        [Range(-100, 100)] public int Reputation;
        public int attackDamage = 5;

        void Update()
        {
            if (EquipmentManager.Instance.GetWeapon() != null && Input.GetMouseButtonDown(0)) //0 = left mouse button
            {
                Attack();
            }
            // Exemplo: Desequipar o item atualmente equipado como arma ao pressionar a tecla Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (EquipmentManager.Instance.GetWeapon() == null && InventorySystem.Instance.GetItems().Count > 0)
                {
                    EquipmentManager.Instance.Equip(InventorySystem.Instance.GetItems()[0]);
                }
                else
                {
                    EquipmentManager.Instance.Unequip(EquipmentManager.Instance.GetWeapon());
                }
            }


            }
        void Attack()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageableEntity))
                {
                    damageableEntity.TakeDamage(attackDamage);
                }
            }
        }
    }

}