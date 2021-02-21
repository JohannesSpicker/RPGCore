using System;
using UnityEngine;

namespace Core.Inventory.Data
{
    [Serializable, CreateAssetMenu(fileName = "New ItemType", menuName = "Core/Inventory/ItemType", order = 0)]
    public class ItemType : ScriptableObject { }
}