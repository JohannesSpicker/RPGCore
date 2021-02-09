using System;
using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    public class ItemSlot : IItemContainer<Item>
    {
        public Item Item   { get; private set; }
        public uint Amount { get; private set; }

        public bool IsEmpty => Item == null || Amount == 0;

        public uint Contains(Item item) => Item == item ? Amount : 0;

        public uint Add(Item item, uint amount)
        {
            if (Item != null && Item != item)
                return amount;

            Item   =  item;
            Amount += amount;

            return 0;
        }

        public uint Remove(Item item, uint amount)
        {
            var remove = Math.Min(amount, Contains(item));

            Amount -= remove;

            if (Amount == 0)
                Item = null;

            return amount - remove;
        }

        public void Clear()
        {
            Item   = null;
            Amount = 0;
        }
    }
}