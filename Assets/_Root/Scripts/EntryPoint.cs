using Game.Factory;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_gameData.SpeedCar, _gameData.JumpPower, _gameData.InitialState, 
            _gameData.TransportType, _gameData.Settings, _gameData.ProductLibrary);
        var inventoryFactory = new InventoryFactory(_placeForUi, profilePlayer);
        var shedFactory = new ShedFactory(_placeForUi, profilePlayer, inventoryFactory);
        _mainController = new MainController(_placeForUi, profilePlayer, shedFactory);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}