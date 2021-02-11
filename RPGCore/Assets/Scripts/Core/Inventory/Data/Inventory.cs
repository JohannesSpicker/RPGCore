using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    [Serializable]
    public class Inventory : IItemContainer<Item>
    {
        private List<ItemType>   allowedTypes;
        public  Action<ItemSlot> onSlotAdded;
        public  Action<ItemSlot> onSlotRemoved;

        public Inventory() { allowedTypes                                 = new List<ItemType>(); }
        public Inventory(List<ItemType> allowedTypes) { this.allowedTypes = allowedTypes; }

        public List<ItemSlot> Slots { get; } = new List<ItemSlot>();

        public bool IsEmpty => Slots?.Count == 0 || Slots.Where(s => !s.IsEmpty).Count() != 0;

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
            foreach (ItemSlot slot in Slots)
                slot.Clear();

            Slots.Clear();
        }

        private bool Allows(ItemType itemType) => allowedTypes.Count == 0 || allowedTypes.Contains(itemType);

        #endregion

        #region ItemSlots

        private ItemSlot CreateItemSlot()
        {
            ItemSlot slot = new ItemSlot();
            slot.onEmpty += KillSlot;

            Slots.Add(slot);
            onSlotAdded?.Invoke(slot);

            return slot;
        }

        private void KillSlot(ItemSlot slot)
        {
            Slots.Remove(slot);
            onSlotRemoved?.Invoke(slot);
        }

        private void CullEmptySlots()
        {
            List<ItemSlot> emptySlots = new List<ItemSlot>();

            foreach (ItemSlot slot in Slots.Where(s => s.IsEmpty))
                emptySlots.Add(slot);

            foreach (ItemSlot slot in emptySlots)
                KillSlot(slot);
        }

        #endregion
    }
}