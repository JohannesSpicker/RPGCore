using System;
using Core.Inventory.Data;
using TMPro;
using UnityEngine;

namespace Core.Inventory.Displays
{
    public class SlotDisplay : MonoBehaviour
    {
        private TextMeshPro amountDisplay;

        private TextMeshPro nameDisplay;
        public  ItemSlot    Slot { get; private set; }

        public Action<SlotDisplay> onSelect;
        
        public void Setup(ItemSlot slot)
        {
            Slot = slot;

            RefreshAmountDisplay(slot.Amount);
            RefreshItemDisplay(slot.Item);

            slot.onAmountChanged += RefreshAmountDisplay;
            slot.onItemChanged   += RefreshItemDisplay;
        }

        private void RefreshAmountDisplay(uint amount) => amountDisplay.text = amount.ToString();
        private void RefreshItemDisplay(Item   item)   => nameDisplay.text = item.ToString();

        public void Select() => onSelect?.Invoke(this);
    }
}