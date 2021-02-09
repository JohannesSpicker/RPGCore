using Core.Inventory.Interfaces;

namespace Core.Inventory.Data
{
    /// <summary>
    ///     Flyweight
    /// </summary>
    public class Item : IItem
    {
        private ItemType itemType;

        public Item(ItemType itemType) { this.itemType = itemType; }
    }
}