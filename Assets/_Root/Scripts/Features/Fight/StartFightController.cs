using Profile;
using Tool;
using UnityEngine;

namespace Features.Fight
{
    internal class StartFightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/StartFightView");

        private readonly StartFightView _startFightView;
        private readonly ProfilePlayer _profilePlayer;

        public StartFightController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _startFightView = LoadView(placeForUI);
            _startFightView.Init(StartFight);
        }

        private void StartFight()
        {
            _profilePlayer.CurrentState.Value = GameState.Fight;
        }

        private StartFightView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI);
            AddGameObject(objectView);
            return objectView.GetComponent<StartFightView>();
        }
    }
}