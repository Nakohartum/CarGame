using _Rewards.Scripts;
using Ui.Settings;
using Features.Fight;
using Game;
using Game.Factory;
using Game.UI;
using Profile;
using Services;
using Shed;
using Tool;
using UnityEngine;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private ShedController _shedController;
    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;
    private ShedFactory _shedFactory;
    private FightController _fightController;
    private RewardController _rewardController;
    private StartFightController _startFightController;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, ShedFactory shedFactory)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _shedFactory = shedFactory;
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        DisposeAllControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        DisposeAllControllers();
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _placeForUi);
                _startFightController = new StartFightController(_profilePlayer, _placeForUi);
                UnityAnalytics.Instance().SendMessage("GameStart game");
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_profilePlayer, _placeForUi);
                break;
            case GameState.Shed:
                _shedController = _shedFactory.Create();
                AddController(_shedController);
                break;
            case GameState.Fight:
                _fightController = new FightController(_profilePlayer, _placeForUi);
                break;
            case GameState.Reward:
                _rewardController = new RewardController(_placeForUi, _profilePlayer);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _shedController?.Dispose();
        _gameController?.Dispose();
        _settingsController?.Dispose();
        _shedController?.Dispose();
        _fightController?.Dispose();
        _startFightController?.Dispose();
        _rewardController?.Dispose();
    }

   
}
