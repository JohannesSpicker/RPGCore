using System;
using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    public class Inventory : IItemContainer<Item>
    {
        public Action<ItemSlot> onSlotAdded;
        public Action<ItemSlot> onSlotRemoved;

        public List<ItemSlot> Slots { get; } = new List<ItemSlot>();

        public bool IsEmpty => Slots?.Count == 0 || Slots.Where(s => s.IsEmpty).Count() != 0;

        public uint Contains(Item item)
        {
            uint amount = 0;

            foreach (ItemSlot slot in Slots)
                amount += slot.Contains(item);

            return amount;
        }

        public uint Add(Item item, uint amount)
        {
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

        public ItemSlot CreateItemSlot()
        {
            ItemSlot slot = new ItemSlot();

            Slots.Add(slot);
            onSlotAdded?.Invoke(slot);

            return slot;
        }

        private void CullEmptySlots()
        {
            List<ItemSlot> emptySlots = new List<ItemSlot>();

            foreach (ItemSlot slot in Slots.Where(s => s.IsEmpty))
                emptySlots.Add(slot);

            foreach (ItemSlot slot in emptySlots)
            {
                Slots.Remove(slot);
                onSlotRemoved?.Invoke(slot);
            }
        }
    }
}