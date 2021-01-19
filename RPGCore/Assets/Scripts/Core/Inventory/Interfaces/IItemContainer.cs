namespace Core.Inventory.Interfaces
{
    public interface IItemContainer<in T> where T : IItem
    {
        uint Contains(T item, uint amount);
        uint Add(T      item, uint amount);
        uint Remove(T   item, uint amount);
        uint Clear();
    }

    public interface IItem { }

    public interface IInventory<in T> : IItemContainer<T> where T : IItem
    {
        
    }
}