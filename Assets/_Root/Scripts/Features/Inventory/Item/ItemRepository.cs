using System.Collections.Generic;
using Game;

namespace Inventory
{
    internal interface IItemRopistory : IRepository
    {
        IReadOnlyDictionary<string, IItem> Items { get; }
    }
    internal class ItemRepository : Repository<string, IItem, ItemConfig>, IItemRopistory
    {

        public ItemRepository(IEnumerable<ItemConfig> configs) : base(configs)
        {
            
        }

        protected override IItem CreateItem(ItemConfig config)
        {
            return new Item(config.ID, new ItemInfo(config.Title, config.Icon));
        }

        protected override string GetKey(ItemConfig config) => config.ID;
    }
}