using System.Collections.Generic;
using UnityEngine;

namespace Shed
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfigDataSource), menuName = "Configs/Shed/" + nameof(UpgradeItemConfigDataSource),
        order = 0)]
    internal class UpgradeItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemConfigs;

        public IReadOnlyList<UpgradeItemConfig> ItemConfigs => _itemConfigs;
    }
}