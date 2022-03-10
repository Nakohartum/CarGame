using Inventory;
using UnityEngine;

namespace Ability
{
    internal interface IAbilityItemConfig
    {
        string ID { get; }
        Sprite Icon { get; }
        AbilityType AbilityType { get; }
        GameObject Projectile { get; }
        float Value { get; }
    }
    [CreateAssetMenu(fileName = nameof(AbilityItemConfig), menuName = "Configs/Ability/"+nameof(AbilityItemConfig), order = 0)]
    public class AbilityItemConfig : ScriptableObject, IAbilityItemConfig
    {
        [SerializeField] private ItemConfig _itemConfig;
        [field: SerializeField] public AbilityType AbilityType { get; private set; }
        [field: SerializeField] public GameObject Projectile { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public string ID => _itemConfig.ID;
        public Sprite Icon => _itemConfig.Icon;
    }
}