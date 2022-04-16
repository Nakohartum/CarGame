using Game.Factory;
using Profile;
using PushNotification.Settings;
using Tool.PushNotification;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private NotificationSettings _notificationSettings;
    private MainController _mainController;
    
    
    

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(_gameData.SpeedCar, _gameData.JumpPower, _gameData.InitialState, 
            _gameData.TransportType, _gameData.Settings, _gameData.ProductLibrary);
        var inventoryFactory = new InventoryFactory(_placeForUi, profilePlayer);
        var shedFactory = new ShedFactory(_placeForUi, profilePlayer, inventoryFactory);
        var schedulerFactory = new NotificationSchedulerFactory(_notificationSettings);
        var scheduler = schedulerFactory.CreateScheduler();
        _mainController = new MainController(_placeForUi, profilePlayer, shedFactory, scheduler, _notificationSettings);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}