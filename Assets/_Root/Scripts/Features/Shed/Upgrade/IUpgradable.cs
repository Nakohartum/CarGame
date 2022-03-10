namespace Shed
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        float JumpPower { get; set; }
        void Restore();
    }
}