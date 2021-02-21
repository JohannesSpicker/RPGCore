using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Data;
using Core.Tools;
using UnityEngine;

namespace Core.Inventory.Displays
{
    public class InventoryDisplay : MonoBehaviour
    {
        public static readonly List<InventoryDisplay> s_inventoryDisplays = new List<InventoryDisplay>();

        #region Selection

        private static SlotDisplay selectedSlotDisplay;

        #endregion

        [SerializeField] private SlotDisplay    slotDisplayPrefab;
        private                  Data.Inventory inventory;

        private PrefabObjectPool<SlotDisplay> slotDisplays;

        private PrefabObjectPool<SlotDisplay> SlotDisplays => slotDisplays ?? CreateObjectPool();
        
        private void OnEnable()  => s_inventoryDisplays.Add(this);
        private void OnDisable() => s_inventoryDisplays.Remove(this);

        public void Setup(Data.Inventory inventory)
        {
            if (this.inventory != null)
                Deregister();

            this.inventory = inventory;

            Register();

            foreach (ItemSlot slot in inventory.Slots)
                AddSlotDisplay(slot);
        }

        private void Register()
        {
            inventory.OnSlotAdded   += AddSlotDisplay;
            inventory.OnSlotRemoved += RemoveSlotDisplay;
        }

        private void Deregister()
        {
            inventory.OnSlotAdded   -= AddSlotDisplay;
            inventory.OnSlotRemoved -= RemoveSlotDisplay;
        }

        private void AddSlotDisplay(ItemSlot slot) => SlotDisplays.Next().Setup(slot);

        private void RemoveSlotDisplay(ItemSlot slot)
        {
            foreach (SlotDisplay slotDisplay in SlotDisplays.inUse.Where(s => s.Slot == slot))
                slotDisplays.Release(slotDisplay);
        }

        private PrefabObjectPool<SlotDisplay> CreateObjectPool()
        {
            slotDisplays = new PrefabObjectPool<SlotDisplay>(slotDisplayPrefab, transform);

            return slotDisplays;
        }
    }
}