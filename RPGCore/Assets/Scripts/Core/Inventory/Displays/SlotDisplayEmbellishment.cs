using Core.Inventory.Data;
using UnityEngine;

namespace Core.Inventory.Displays
{
    [RequireComponent(typeof(SlotDisplay))]
    public abstract class SlotDisplayEmbellishment : MonoBehaviour
    {
        private SlotDisplay slotDisplay;

        protected virtual void Awake()
        {
            slotDisplay = GetComponent<SlotDisplay>();

            slotDisplay.OnSelect             += OnSelect;
            slotDisplay.Slot.OnAmountChanged += OnAmountChanged;
            slotDisplay.Slot.OnItemChanged   += OnItemChanged;
        }

        protected virtual void OnDestroy()
        {
            slotDisplay.OnSelect             -= OnSelect;
            slotDisplay.Slot.OnAmountChanged -= OnAmountChanged;
            slotDisplay.Slot.OnItemChanged   -= OnItemChanged;
        }

        protected abstract void OnSelect(SlotDisplay slotDisplay);
        protected abstract void OnAmountChanged(uint amount);
        protected abstract void OnItemChanged(Item   item);
    }
}