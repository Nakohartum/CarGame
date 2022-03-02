using UnityEngine.Events;

namespace Services
{
    public interface IAdsService
    {
        IAdsPlayer InterstitionalPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        UnityEvent Initialized { get; }
        bool IsInitialized { get; }
    }
}