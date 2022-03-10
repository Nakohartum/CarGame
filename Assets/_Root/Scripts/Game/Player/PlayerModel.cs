using Shed;

namespace Game
{
    public class PlayerModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJumpPower;
        public float Speed { get; set; }
        public float JumpPower { get; set; }

        public PlayerModel(float speed, float jumpPower)
        {
            _defaultSpeed = speed;
            _defaultJumpPower = jumpPower;
            Speed = speed;
            JumpPower = jumpPower;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpPower = _defaultJumpPower;
        }
    }
}