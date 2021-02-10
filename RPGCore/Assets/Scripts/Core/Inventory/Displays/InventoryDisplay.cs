using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Data;
using Core.Tools;
using UnityEngine;
using UnityTemplateProjects.Core.Inventory.Displays;

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

        private void Awake() => slotDisplays = new PrefabObjectPool<SlotDisplay>(slotDisplayPrefab, transform);

        private void OnEnable()  => s_inventoryDisplays.Add(this);
        private void OnDisable() => s_inventoryDisplays.Remove(this);

        public void Setup(Data.Inventory inventory)
        {
            this.inventory = inventory;

            slotDisplays.ReleaseAll();

            foreach (ItemSlot slot in inventory.Slots)
                AddSlotDisplay(slot);
        }

        private void Register()
        {
            inventory.onSlotAdded   += AddSlotDisplay;
            inventory.onSlotRemoved += RemoveSlotDisplay;
        }

        private void Deregister()
        {
            inventory.onSlotAdded   -= AddSlotDisplay;
            inventory.onSlotRemoved -= RemoveSlotDisplay;
        }

        private void AddSlotDisplay(ItemSlot slot) => slotDisplays.Next().Setup(slot);

        private void RemoveSlotDisplay(ItemSlot slot)
        {
            foreach (SlotDisplay slotDisplay in slotDisplays.inUse.Where(s => s.Slot == slot))
                slotDisplays.Release(slotDisplay);
        }
    }
}