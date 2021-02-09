namespace Core.Inventory.Interfaces
{
    public interface IItemContainer<in T> where T : IItem
    {
        /// Returns amount of items equal to input.
        uint Contains(T item);
        
        /// <summary>
        /// Add items to the container.
        /// Returns amount of items not added.
        /// </summary>
        uint Add(T      item, uint amount);
        
        /// <summary>
        /// Remove items from the container.
        /// Returns amount of items not removed.
        /// </summary>
        uint Remove(T   item, uint amount);
        
        /// <summary>
        /// Remove all items from the container. 
        /// </summary>
        void Clear();
    }
}