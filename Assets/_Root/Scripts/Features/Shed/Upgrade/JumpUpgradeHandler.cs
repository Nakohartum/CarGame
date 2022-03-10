namespace Shed
{
    internal class JumpUpgradeHandler : IUpgradeHandler
    {
        private readonly float _jumpPower;

        public JumpUpgradeHandler(float jumpPower)
        {
            _jumpPower = jumpPower;
        }
        public void Upgrade(IUpgradable upgradable)
        {
            upgradable.JumpPower += _jumpPower;
        }
    }
}