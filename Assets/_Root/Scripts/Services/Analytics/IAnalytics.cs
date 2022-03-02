using System.Collections.Generic;

namespace Services
{
    internal interface IAnalytics
    {
        void SendMessage(string alias, IDictionary<string, object> eventData = null);
    }
}