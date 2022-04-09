using Game.Car;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/KeyBoardMove");
        private BaseInputView _view;
        private ContactPoller _contactPoller;


        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> jumpMove,
            PlayerModel model, TransportController transportController)
        {
            _contactPoller = new ContactPoller(transportController);
            AddController(_contactPoller);
            _view = LoadView();
            _view.Init(leftMove, rightMove, jumpMove, model.Speed, model.JumpPower, transportController, _contactPoller);
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
