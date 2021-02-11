using System;
using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    [Serializable]
    public class ItemSlot : IItemContainer<Item>
    {
        private uint amount;
        private Item item;

        public Action<uint>     onAmountChanged;
        public Action<Item>     onItemChanged;
        public Action<ItemSlot> onEmpty;

        public uint Amount
        {
            get => amount;
            private set
            {
                amount = value;
                onAmountChanged?.Invoke(amount);

                if (amount == 0)
                    onEmpty?.Invoke(this);
            }
        }

        public Item Item
        {
            get => item;
            private set
            {
                item = value;
                onItemChanged?.Invoke(item);
            }
        }

        public bool IsEmpty             => Item == null || Amount == 0;
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
            uint remove = Math.Min(amount, Contains(item));

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

        public void HandOver(ItemSlot receiver, uint amount)
        {
            uint available = Math.Min(amount, Amount);
            uint moved     = available - receiver.Add(Item, available);
            Remove(Item, moved);
        }
    }
}