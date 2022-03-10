namespace Inventory
{
    internal class Item : IItem
    {
        public string ID { get; }
        public ItemInfo ItemInfo { get; }

        public Item(string id, ItemInfo itemInfo)
        {
            ID = id;
            ItemInfo = itemInfo;
        }
    }
}