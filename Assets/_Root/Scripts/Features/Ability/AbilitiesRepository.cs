using System;
using System.Collections.Generic;
using Game;

namespace Ability
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }
    internal class AbilitiesRepository : Repository<string, IAbility, AbilityItemConfig>, IAbilitiesRepository
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs) : base(configs)
        {
        }

        protected override IAbility CreateItem(AbilityItemConfig config)
        {
            switch (config.AbilityType)
            {
                case AbilityType.Gun:
                    return new GunAbility(config);
                default:
                    return StubAbility.Default;
            }
        }

        protected override string GetKey(AbilityItemConfig config)
        {
            return config.ID;
        }
    }
}