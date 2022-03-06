namespace Inventory
{
    internal interface IItem
    {
        string ID { get; }
        ItemInfo ItemInfo { get; }
    }
}