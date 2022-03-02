using Services;
using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    [SerializeField]private GameState InitialState;
    [SerializeField] private TransportType TransportType;
    [SerializeField] private Settings _settings;
    [SerializeField] private ProductLibrary _productLibrary;
    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Awake()
    {
        var analytics = UnityAnalytics.Instance();
        var unityAdsService = UnityAdsService.Instance(_settings);
        var iapService = IAPService.Instance(_productLibrary);
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState, TransportType, analytics, unityAdsService, iapService);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
