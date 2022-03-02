using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Services
{
    internal class UnityAnalytics : IAnalytics
    {
        private static UnityAnalytics _instance;
        public static UnityAnalytics Instance()
        {
            if (_instance == null)
            {
                _instance = new UnityAnalytics();
            }

            return _instance;
        }
        private UnityAnalytics()
        {
            
        }
        public void SendMessage(string alias, IDictionary<string, object> eventData = null)
        {
            if (eventData == null)
            {
                eventData = new Dictionary<string, object>();
            }

            UnityEngine.Analytics.Analytics.CustomEvent(alias, eventData);
        }
    }
}
