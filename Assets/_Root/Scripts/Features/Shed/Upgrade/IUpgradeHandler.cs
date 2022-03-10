namespace Shed
{
    internal interface IUpgradeHandler
    {
        void Upgrade(IUpgradable upgradable);
    }
}