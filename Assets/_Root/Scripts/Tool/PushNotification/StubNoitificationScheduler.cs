using PushNotification.Settings;
using UnityEngine;

namespace Tool.PushNotification
{
    internal class StubNoitificationScheduler : INotificationScheduler
    {
        public void ScheduleNotification(NotificationData notificationData)
        {
            Debug.Log($"[{GetType()}] {notificationData}");
        }
    }
}