using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory.Data
{
    [CreateAssetMenu(fileName = "New StartInventory", menuName = "Core/Inventory/StartInventory", order = 0)]
    public class StartInventory : ScriptableObject
    {
        [SerializeField] private Inventory      inventory;
        [SerializeField] private List<ItemType> allowedTypes;

        public Inventory CreateInventory()
        {
            Inventory newInventory = new Inventory(allowedTypes);

            foreach (ItemSlot slot in inventory.Slots)
                newInventory.Add(slot.Item, slot.Amount);

            return newInventory;
        }
    }
}