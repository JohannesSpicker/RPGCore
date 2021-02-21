using System;
using Core.Inventory.Interfaces;
using UnityEngine;

namespace Core.Inventory.Data
{
    [Serializable]
    public class ItemSlot : IItemContainer<Item>
    {
        [SerializeField] private uint amount;
        [SerializeField] private Item item;

        public uint Amount
        {
            get => amount;
            private set
            {
                amount = value;
                OnAmountChanged?.Invoke(amount);
            }
        }

        public Item Item
        {
            get => item;
            private set
            {
                item = value;
                OnItemChanged?.Invoke(item);
            }
        }

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

        public event Action<uint> OnAmountChanged;
        public event Action<Item> OnItemChanged;

        public void HandOver(ItemSlot receiver, uint amount)
        {
            uint available = Math.Min(amount, Amount);
            uint moved     = available - receiver.Add(Item, available);
            Remove(Item, moved);
        }
    }
}