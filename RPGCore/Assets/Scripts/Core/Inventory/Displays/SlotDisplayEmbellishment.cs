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

            slotDisplay.onSelect             += OnSelect;
            slotDisplay.Slot.onEmpty         += OnEmpty;
            slotDisplay.Slot.onAmountChanged += OnAmountChanged;
            slotDisplay.Slot.onItemChanged   += OnItemChanged;
        }

        protected virtual void OnDestroy()
        {
            slotDisplay.onSelect             -= OnSelect;
            slotDisplay.Slot.onEmpty         -= OnEmpty;
            slotDisplay.Slot.onAmountChanged -= OnAmountChanged;
            slotDisplay.Slot.onItemChanged   -= OnItemChanged;
        }

        protected abstract void OnSelect(SlotDisplay slotDisplay);
        protected abstract void OnEmpty(ItemSlot     slot);
        protected abstract void OnAmountChanged(uint amount);
        protected abstract void OnItemChanged(Item   item);
    }
}