using System;
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
        public Sprite   sprite;
        public ItemType itemType;
        
        
    }
    
    [CreateAssetMenu(fileName = "New Item", menuName = "Core/Inventory/ItemWeapon", order = 0)]
    public class Weapon : Item
    {
        public Tuple<int, int> damage;
    }
}