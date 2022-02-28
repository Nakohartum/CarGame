using Game;
using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    [SerializeField]private GameState InitialState;
    [SerializeField] private TransportType TransportType;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState, TransportType);
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
