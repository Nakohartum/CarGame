using System;
using Services;
using Game;
using Game.Boat;
using Game.Car;
using Inventory;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public Settings Settings { get; }
        public ProductLibrary ProductLibrary { get; }
        public readonly TransportType TransportType;
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly PlayerModel PlayerModel;
        public readonly InventoryModel Inventory;
        public Gold Gold;

        public ProfilePlayer(float speedCar, float jumpSpeed, GameState initialState, TransportType transportType, 
            Settings settings, ProductLibrary productLibrary) : this(speedCar, jumpSpeed)
        {
            Settings = settings;
            ProductLibrary = productLibrary;
            TransportType = transportType;
            CurrentState.Value = initialState;
            Inventory = new InventoryModel();
            Gold = new Gold();
        }

        public ProfilePlayer(float speedCar, float jumpSpeed)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            PlayerModel = ChooseTransport(speedCar, jumpSpeed);
        }

        private PlayerModel ChooseTransport(float speed, float jumpSpeed)
        {
            PlayerModel playerModel = new PlayerModel(speed, jumpSpeed);
            switch (TransportType)
            {
                case TransportType.Car:
                    playerModel = new CarModel(speed, jumpSpeed); 
                    break;
                case TransportType.Boat:
                    playerModel = new BoatModel(speed, jumpSpeed); 
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
