using System;
using UnityEngine;


namespace PushNotification.Settings
{
    [CreateAssetMenu(fileName = nameof(NotificationSettings), menuName = "Configs/Notifications/"+nameof(NotificationSettings))]
    internal class NotificationSettings : ScriptableObject
    {
        [field: SerializeField] public ChannelSettings[] ChannelSettings { get; private set; }
        [field: SerializeField] public NotificationData[] Notifications { get; private set; }
    }

    [Serializable]

    internal struct ChannelSettings
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [SerializeField] private Importance _importance;

#if UNITY_ANDROID
        public Unity.Notifications.Android.Importance Importance => (Unity.Notifications.Android.Importance) _importance;
#else
        public Importance Importance => _importance;
#endif
    }
    
    [Serializable]
    
    internal struct NotificationData
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public NotificationRepeat RepeatType { get; private set; }
        [field: SerializeField] public Date FireTime { get; private set; }
        [field: SerializeField] public Span RepeatTime { get; private set; }

        public override string ToString()
        {
            return $"{ID} : {Title}. {Text}. {RepeatType:F}, {FireTime}, {RepeatTime}";
        }
    }

    internal enum Importance
    {
        None = 0,
        Low = 1,
        Default = 2,
        High = 3
    }

    internal enum NotificationRepeat
    {
        Once,
        Repeatable
    }

    [Serializable]
    internal struct Date
    {
        [field: SerializeField] public int Year { get; private set; }
        [field: SerializeField] public int Month { get; private set; }
        [field: SerializeField] public int Day { get; private set; }
        [field: SerializeField] public int Hour { get; private set; }
        [field: SerializeField] public int Minute { get; private set; }

        public static implicit operator DateTime(Date date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, default);
        }
    }

    [Serializable]
    internal struct Span
    {
        [field: SerializeField] public int Seconds { get; private set; }

        public override string ToString()
        {
            return Seconds.ToString();
        }

        public static implicit operator TimeSpan(Span span) => TimeSpan.FromSeconds(span.Seconds);
    }
}