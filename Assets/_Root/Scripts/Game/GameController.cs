using System;
using Game.Boat;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;

namespace Game
{
    internal class GameController : BaseController
    {
        public GameController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.PlayerModel);
            AddController(inputGameController);

            CreatePlayerController(profilePlayer);
        }

        private void CreatePlayerController(ProfilePlayer profilePlayer)
        {
            switch (profilePlayer.TransportType)
            {
                case TransportType.Car:
                    AddController(new CarController());
                    break;
                case TransportType.Boat:
                    AddController(new BoatController());
                    break;
                case TransportType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
