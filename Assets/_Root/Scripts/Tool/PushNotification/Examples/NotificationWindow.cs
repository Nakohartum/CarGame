using System;
using PushNotification.Settings;
using UnityEngine;
using UnityEngine.UI;


namespace Tool.PushNotification.Examples
{
    internal class NotificationWindow : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private NotificationSettings _notificationSettings;

        [Header("Scene Components")] 
        [SerializeField] private Button _buttonNotification;

        private NotificationSchedulerFactory _factory;
        private INotificationScheduler _scheduler;

        private void Awake()
        {
            _factory = new NotificationSchedulerFactory(_notificationSettings);
            _scheduler = _factory.CreateScheduler();
        }

        private void OnEnable()
        {
            _buttonNotification.onClick.AddListener(CreateNotification);
        }

        private void OnDisable()
        {
            _buttonNotification.onClick.RemoveAllListeners();
        }

        private void CreateNotification()
        {
            foreach (var notificationSetting in _notificationSettings.Notifications)
            {
                _scheduler.ScheduleNotification(notificationSetting);
            }
        }
    }
}