using Core.Inventory.Data;
using UnityEngine;

namespace Core.Inventory.Displays
{
    [RequireComponent(typeof(InventoryDisplay))]
    public class InventoryProvider : MonoBehaviour
    {
        [SerializeField] private StartInventory startInventory;

        private void Awake() => GetComponent<InventoryDisplay>().Setup(startInventory.CreateInventory());
    }
}