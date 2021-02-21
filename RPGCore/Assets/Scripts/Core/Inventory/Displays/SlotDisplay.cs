using System;
using Core.Inventory.Data;
using TMPro;
using UnityEngine;

namespace Core.Inventory.Displays
{
    public class SlotDisplay : MonoBehaviour
    {
       [SerializeField] private TMP_Text  amountDisplay;

       [SerializeField] private TMP_Text nameDisplay;
        public                  ItemSlot Slot { get; private set; }

        public event Action<SlotDisplay> OnSelect;
        
        public void Setup(ItemSlot slot)
        {
            Slot = slot;

            RefreshAmountDisplay(slot.Amount);
            RefreshItemDisplay(slot.Item);

            slot.OnAmountChanged += RefreshAmountDisplay;
            slot.OnItemChanged   += RefreshItemDisplay;
        }

        private void RefreshAmountDisplay(uint amount) => amountDisplay.text = amount.ToString();
        private void RefreshItemDisplay(Item   item)   => nameDisplay.text = item.ToString();

        public void Select() => OnSelect?.Invoke(this);
    }
}