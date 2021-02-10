using Core.Inventory.Data;

namespace Core.Character
{
    public class Character
    {
        private Inventory.Data.Inventory inventory;
        private StartInventory           startInventory;

        private void SetupInventory() => inventory = startInventory.CreateInventory();
    }
}