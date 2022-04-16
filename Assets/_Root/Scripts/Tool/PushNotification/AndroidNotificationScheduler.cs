using System;
using System.Collections;
using System.Collections.Generic;
using PushNotification.Settings;
using Tool.PushNotification;
using Unity.Notifications.Android;
using UnityEngine;

internal class AndroidNotificationScheduler : INotificationScheduler
{
    public void RegisterChannel(ChannelSettings channelSettings)
    {
#if UNITY_ANDROID
        var androidNotificationChannel = new AndroidNotificationChannel(channelSettings.ID, channelSettings.Name,
            channelSettings.Description, channelSettings.Importance);
        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
#endif
    }
    public void ScheduleNotification(NotificationData notificationData)
    {
#if UNITY_ANDROID
        var androidNotification = CreateAndroidNotification(notificationData);
        AndroidNotificationCenter.SendNotification(androidNotification, notificationData.ID);
#endif
    }

#if UNITY_ANDROID
    

    private AndroidNotification CreateAndroidNotification(NotificationData notificationData)
    {
        switch (notificationData.RepeatType)
        {
            case NotificationRepeat.Once:
                return new AndroidNotification(notificationData.Title, notificationData.Text,
                    notificationData.FireTime);
            case NotificationRepeat.Repeatable:
                return new AndroidNotification(notificationData.Title, notificationData.Text,
                    notificationData.FireTime, notificationData.RepeatTime);
            default:
                throw new ArgumentOutOfRangeException();
        }        
    }
#endif
}
