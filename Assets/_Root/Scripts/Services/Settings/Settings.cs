using Services;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = nameof(Settings), menuName = "Configs/"+nameof(Settings))]
    internal class Settings : ScriptableObject
    {
        [Header("ID")] 
        [SerializeField] private string _androidID;
        [SerializeField] private string _iosID;
        
        [field: Header("Settings")]
        [field: SerializeField] public AdsPlayerSettings Interstitial { get; private set; }
        [field: SerializeField] public AdsPlayerSettings Rewarded { get; private set; }
        
        [field: Header("Settings")]
        [field: SerializeField] public bool TestMode { get; private set; }
        [field: SerializeField] public bool EnanblePerPlacementMode { get; private set; }

        public string GameID
        {
            get
            {
#if UNITY_EDITOR
                return _androidID;
#else
            switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return _androidID;
                    case RuntimePlatform.IPhonePlayer:
                        return _iosID;
                    default:
                        return "";
                }
#endif
            }
        }
    }
}