using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Interfaces;
using UnityEngine;

namespace Core.Inventory.Data
{
    [Serializable]
    public class Inventory : IItemContainer<Item>
    {
        [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();
        private                  List<ItemType> allowedTypes;

        public Inventory() { allowedTypes = new List<ItemType>(); }

        public Inventory(List<ItemType> allowedTypes) { this.allowedTypes = allowedTypes; }

        public List<ItemSlot> Slots => slots;

        public bool IsEmpty => Slots?.Count == 0 || Slots.Where(s => !s.IsEmpty).Count() != 0;

        public event Action<ItemSlot> OnSlotAdded;
        public event Action<ItemSlot> OnSlotRemoved;
        public event Action           OnSlotsCleared;

        #region IItemContainer

        public uint Contains(Item item)
        {
            uint amount = 0;

            foreach (ItemSlot slot in Slots)
                amount += slot.Contains(item);

            return amount;
        }

        public uint Add(Item item, uint amount)
        {
            if (!Allows(item.itemType))
                return amount;

            foreach (ItemSlot slot in Slots.Where(s => s.Item == item))
                amount = slot.Add(item, amount);

            if (0 < amount)
                amount = CreateItemSlot().Add(item, amount);

            return amount;
        }

        public uint Remove(Item item, uint amount)
        {
            foreach (ItemSlot slot in Slots.Where(s => s.Item == item))
                amount = slot.Remove(item, amount);

            CullEmptySlots();

            return amount;
        }

        public void Clear()
        {
            List<ItemSlot> emptySlots = Slots.Where(s => s.IsEmpty).ToList();

            foreach (ItemSlot slot in emptySlots)
                slot.Clear();

            Slots.Clear();

            OnSlotsCleared?.Invoke();
        }

        private bool Allows(ItemType itemType) => allowedTypes.Count == 0 || allowedTypes.Contains(itemType);

        #endregion

        #region ItemSlots

        private ItemSlot CreateItemSlot()
        {
            ItemSlot slot = new ItemSlot();

            Slots.Add(slot);
            OnSlotAdded?.Invoke(slot);

            return slot;
        }

        private void KillSlot(ItemSlot slot)
        {
            Slots.Remove(slot);
            OnSlotRemoved?.Invoke(slot);
        }

        private void CullEmptySlots() => Slots.Where(s => s.IsEmpty).ToList().ForEach(slot => KillSlot(slot));

        #endregion
    }
}