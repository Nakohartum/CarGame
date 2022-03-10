using Inventory;
using UnityEngine;

namespace Shed
{
    [CreateAssetMenu(fileName = nameof(UpgradeItemConfig), menuName = "Configs/Shed/" + nameof(UpgradeItemConfig),
        order = 0)]
    internal class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;

        [field: SerializeField] public UpgradeType UpgradeType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string ID => _itemConfig.ID;
    }
}