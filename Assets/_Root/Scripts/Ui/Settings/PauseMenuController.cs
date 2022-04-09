using Profile;
using Tool;
using UnityEngine;

namespace Ui.Settings
{
    internal class PauseMenuController : BaseController
    {
        private ResourcePath _resourcePath = new ResourcePath("Prefabs/PauseMenuView");
        private ProfilePlayer _profilePlayer;
        private PauseMenuView _pauseMenuView;

        public PauseMenuController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _pauseMenuView = CreateView(placeForUI);
            SubscribeView(_pauseMenuView);
        }

        private PauseMenuView CreateView(Transform placeForUI)
        {
            var prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            var go = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(go);
            return go.GetComponent<PauseMenuView>();
        }

        private void SubscribeView(PauseMenuView pauseMenuView)
        {
            pauseMenuView.ButtonPause.onClick.AddListener(pauseMenuView.Panel.ShowPopup);
            pauseMenuView.Panel.ButtonCloseGame.onClick.AddListener(CloseGame);
        }

        private void CloseGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            UnSubscribeView(_pauseMenuView);
        }

        private void UnSubscribeView(PauseMenuView pauseMenuView)
        {
            pauseMenuView.ButtonPause.onClick.RemoveAllListeners();
            pauseMenuView.Panel.ButtonCloseGame.onClick.RemoveAllListeners();
        }
    }
}