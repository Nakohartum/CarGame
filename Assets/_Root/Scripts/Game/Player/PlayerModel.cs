using Shed;

namespace Game
{
    public class PlayerModel : IUpgradable
    {
        private readonly float _defaultSpeed;

        public float Speed { get; set; }

        public PlayerModel(float speed)
        {
            _defaultSpeed = speed;
            Speed = speed;
        }

        public void Restore()
        {
            Speed = _defaultSpeed;
        }
    }
}