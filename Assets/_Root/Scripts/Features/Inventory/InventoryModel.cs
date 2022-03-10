using System.Collections.Generic;

namespace Inventory
{
    internal interface IInventoryModel
    {
        IReadOnlyList<string> EquippedItems { get; }
        void EquipItem(string item);
        void UnequipItem(string item);
        bool IsEquipped(string item);
    }
    internal class InventoryModel : IInventoryModel
    {
        private readonly List<string> _equippedItems = new List<string>();
        public IReadOnlyList<string> EquippedItems => _equippedItems;
        
        public void EquipItem(string item)
        {
            if (!IsEquipped(item))
            {
                _equippedItems.Add(item);
            }    
        }

        public void UnequipItem(string item)
        {
            if (IsEquipped(item))
            {
                _equippedItems.Remove(item);
            }
        }

        public bool IsEquipped(string item)
        {
            return _equippedItems.Contains(item);
        }
    }
}