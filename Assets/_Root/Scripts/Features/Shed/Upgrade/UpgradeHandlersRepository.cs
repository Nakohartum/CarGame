using System;
using System.Collections.Generic;
using Game;

namespace Shed
{
    internal interface IUpgradeHandlersRepository : IRepository
    {
        IReadOnlyDictionary<string, IUpgradeHandler> Items { get; }
    }
    internal class UpgradeHandlersRepository : Repository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        {
        }

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config)
        {
            switch (config.UpgradeType)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeHandler(config.Value);
                case UpgradeType.Jump:
                    return new JumpUpgradeHandler(config.Value);
                default:
                    return StubUpgradeHandler.Default;
            }
        }

        protected override string GetKey(UpgradeItemConfig config)
        {
            return config.ID;
        }
    }
}