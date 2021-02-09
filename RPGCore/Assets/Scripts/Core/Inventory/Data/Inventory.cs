using System.Collections.Generic;
using System.Linq;
using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    public class Inventory : IItemContainer<Item>
    {
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
            {
                ItemSlot newSlot = new ItemSlot();
                amount = newSlot.Add(item, amount);
                Slots.Add(newSlot);
            }

            return amount;
        }

        public uint Remove(Item item, uint amount)
        {
            foreach (ItemSlot slot in Slots.Where(s => s.Item == item))
                amount = slot.Remove(item, amount);

            return amount;
        }

        public void Clear()
        {
            foreach (ItemSlot slot in Slots)
                slot.Clear();
        }
    }
}