using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    [CreateAssetMenu(fileName = nameof(AbilityItemConfigDataSource), menuName = 
        "Configs/Ability/"+nameof(AbilityItemConfigDataSource), order = 0)]

    public class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityItemConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityItemConfigs => _abilityItemConfigs;
    }
}