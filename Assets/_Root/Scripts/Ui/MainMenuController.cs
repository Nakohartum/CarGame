using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Purchasing;
using Object = UnityEngine.Object;

namespace Game.UI
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/mainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, ShowRewarded, ShowIntersitial, Buy);
            _profilePlayer.Gold.Value.SubscribeOnChange(_view.ChangeText);
        }

        private void Buy(Product product)
        {
            _profilePlayer.Gold.Value.Value += 100;
        }


        private void ShowIntersitial()
        {
            _profilePlayer.UnityAdsService.InterstitionalPlayer.Play();
        }

        private void ShowRewarded()
        {
            _profilePlayer.UnityAdsService.RewardedPlayer.Play();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void OpenSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        protected override void OnDispose()
        {
            base.OnDispose();
            _profilePlayer.Gold.Value.UnSubscribeOnChange(_view.ChangeText);
        }
    }
    
    
}
