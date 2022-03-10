namespace Shed
{
    internal class StubUpgradeHandler : IUpgradeHandler
    {
        public static IUpgradeHandler Default = new StubUpgradeHandler();
        public void Upgrade(IUpgradable upgradable)
        {
            
        }
    }
}