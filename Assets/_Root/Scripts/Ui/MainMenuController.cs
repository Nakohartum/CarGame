using Profile;
using PushNotification.Settings;
using Services;
using Tool;
using Tool.PushNotification;
using Ui.Settings;
using UnityEngine;
using Object = UnityEngine.Object;
using Product = UnityEngine.Purchasing.Product;

namespace Game.UI
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/mainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly INotificationScheduler _scheduler;
        private NotificationData _notificationData;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, 
            INotificationScheduler scheduler, NotificationData notificationData)
        {
            _profilePlayer = profilePlayer;
            _scheduler = scheduler;
            _notificationData = notificationData;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, ShowRewarded, ShowIntersitial, OpenShed,Buy, OpenRewards, SendNotification);
            _profilePlayer.Gold.Value.SubscribeOnChange(_view.ChangeText);
            
        }

        private void SendNotification()
        {
            _scheduler.ScheduleNotification(_notificationData);
        }

        private void OpenRewards()
        {
            _profilePlayer.CurrentState.Value = GameState.Reward;
        }

        private void OpenShed()
        {
            _profilePlayer.CurrentState.Value = GameState.Shed;
        }

        private void Buy(Product product)
        {
            _profilePlayer.Gold.Value.Value += 100;
        }


        private void ShowIntersitial()
        {
            UnityAdsService.Instance(_profilePlayer.Settings).InterstitionalPlayer.Play();
        }

        private void ShowRewarded()
        {
            UnityAdsService.Instance(_profilePlayer.Settings).RewardedPlayer.Play();
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
