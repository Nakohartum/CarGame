using System;
using Services;
using Game;
using Game.Boat;
using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public IAPService IAPService;
        public readonly TransportType TransportType;
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly PlayerModel PlayerModel;
        public IAnalytics Analytics;
        public UnityAdsService UnityAdsService;
        public Gold Gold;

        public ProfilePlayer(float speedCar, GameState initialState, TransportType transportType, IAnalytics analytics,
            UnityAdsService unityAdsService, IAPService iapService) : this(speedCar)
        {
            TransportType = transportType;
            CurrentState.Value = initialState;
            Analytics = analytics;
            UnityAdsService = unityAdsService;
            IAPService = iapService;
            Gold = new Gold();
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            PlayerModel = ChooseTransport(speedCar);
        }

        private PlayerModel ChooseTransport(float speed)
        {
            PlayerModel playerModel = new PlayerModel(speed);
            switch (TransportType)
            {
                case TransportType.Car:
                    playerModel = new CarModel(speed); 
                    break;
                case TransportType.Boat:
                    playerModel = new BoatModel(speed); 
                    break;
                case TransportType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return playerModel;
        }
    }
}
