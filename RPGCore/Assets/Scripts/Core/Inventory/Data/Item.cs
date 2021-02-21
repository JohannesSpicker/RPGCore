using Core.Inventory.Interfaces;
using UnityEngine;

namespace Core.Inventory.Data
{
    /// <summary>
    ///     Flyweight
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Core/Inventory/Item", order = 0)]
    public class Item : ScriptableObject, IItem
    {
        public ItemType itemType;
    }
}