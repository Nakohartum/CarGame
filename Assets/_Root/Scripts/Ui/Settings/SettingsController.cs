using Profile;
using Tool;
using UnityEngine;

namespace Game.UI
{
    internal class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/settingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsView _view;

        public SettingsController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.Init(Back);
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private SettingsView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);
            return objectView.GetComponent<SettingsView>();
        }
    }
}