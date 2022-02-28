using Game.Car;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/KeyBoardMove");
        private BaseInputView _view;


        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            PlayerModel model)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, model.Speed);
        }

        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            BaseInputView view = objectView.GetComponent<BaseInputView>();
            return view;
        }
    }
}
