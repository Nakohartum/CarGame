using PushNotification.Settings;

namespace Tool.PushNotification
{
    internal interface INotificationScheduler
    {
        void ScheduleNotification(NotificationData notificationData);
    }
}