using PushNotification.Settings;

namespace Tool.PushNotification
{
    internal class NotificationSchedulerFactory
    {
        private readonly NotificationSettings _settings;

        public NotificationSchedulerFactory(NotificationSettings settings)
        {
            _settings = settings;
        }

        public INotificationScheduler CreateScheduler()
        {
#if UNITY_EDITOR
            return new StubNoitificationScheduler();
#elif UNITY_ANDROID
            var scheduler = new AndroidNotificationScheduler();
            foreach (var channelSetting in _settings.ChannelSettings)
            {
                scheduler.RegisterChannel(channelSetting);
            }

            return scheduler;
#elif UNITY_IOS
            return new IOSNotificationScheduler();
#else
            return new StubNoitificationScheduler();
#endif
        }
    }
}