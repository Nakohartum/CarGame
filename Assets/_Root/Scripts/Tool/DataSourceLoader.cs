using System;
using System.Linq;
using Ability;
using Inventory;
using Shed;

namespace Tool
{
    internal class DataSourceLoader
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath path)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(path);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.ItemConfigs.ToArray();
        }


        public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath dataSourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<AbilityItemConfigDataSource>(dataSourcePath);
            return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.AbilityItemConfigs.ToArray();
        }

        public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath dataSourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<UpgradeItemConfigDataSource>(dataSourcePath);
            return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        }
    }
}