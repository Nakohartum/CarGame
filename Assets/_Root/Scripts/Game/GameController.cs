using System;
using Ability;
using Game.Boat;
using Game.Car;
using Game.Factory;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;
using Ui.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;
        private readonly SubscriptionProperty<float> _jumpMoveDiff;
        private readonly TransportController _transportController;
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly InputGameController _inputGameController;
        private readonly IAbilitiesController _abilitiesController;
        private readonly PauseMenuController _pauseMenuController;
        public GameController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();
            _jumpMoveDiff = new SubscriptionProperty<float>();
            _tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddController(_tapeBackgroundController);
            _transportController = CreatePlayerController(_profilePlayer);
            var abilitiesFactory = new AbilitiesFactory(placeForUI, _transportController);
            _inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, _jumpMoveDiff, profilePlayer.PlayerModel, _transportController);
            AddController(_inputGameController);
            _abilitiesController = abilitiesFactory.Create();
            AddController((AbilitiesController)_abilitiesController);
            _pauseMenuController = new PauseMenuController(_profilePlayer, placeForUI);
            AddController(_pauseMenuController);
        }
        private TransportController CreatePlayerController(ProfilePlayer profilePlayer)
        {
            TransportController transportController = null;
            switch (profilePlayer.TransportType)
            {
                case TransportType.Car:
                    transportController = new CarController();
                    break;
                case TransportType.Boat:
                    transportController = new BoatController();
                    break;
                case TransportType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            AddController(transportController);
            return transportController;
        }
    }
}
