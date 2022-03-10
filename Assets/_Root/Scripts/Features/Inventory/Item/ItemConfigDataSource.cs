using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/Inventory/"+nameof(ItemConfigDataSource), order = 0)]
    public class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemConfigs;

        public IReadOnlyCollection<ItemConfig> ItemConfigs => _itemConfigs;
    }
}