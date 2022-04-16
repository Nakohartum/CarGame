using System;
using PushNotification.Settings;

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif


namespace Tool.PushNotification
{
    internal class IOSNotificationScheduler : INotificationScheduler
    {
        
        public void ScheduleNotification(NotificationData notificationData)
        {
#if UNITY_IOS
            

            var iosNotification = new iOSNotification
            {
                Identifier = notificationData.ID,
                Title = notificationData.Title,
                Body = notificationData.Text,
                Trigger = CreateIOSTrigger(notificationData)
            };
            iOSNotificationCenter.ScheduleNotification(iosNotification);
#endif
        }
#if UNITY_IOS
        
        private iOSNotificationTrigger CreateIOSTrigger(NotificationData notificationData)
        {
            switch (notificationData.RepeatType)
            {
                case NotificationRepeat.Once:
                    return new iOSNotificationCalendarTrigger()
                    {
                        Year = notificationData.FireTime.Year,
                        Month = notificationData.FireTime.Month,
                        Day = notificationData.FireTime.Day,
                        Hour = notificationData.FireTime.Hour,
                        Minute = notificationData.FireTime.Minute
                    };
                case NotificationRepeat.Repeatable:
                    return new iOSNotificationTimeIntervalTrigger()
                    {
                        Repeats = true, TimeInterval = notificationData.RepeatTime
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

#endif
    }

}