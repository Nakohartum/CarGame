using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    internal class AdsPlayerSettings
    {
        [field: SerializeField]
        public bool IsEnabled { get; private set; }

        [SerializeField] private string _androidID;
        [SerializeField] private string _iosID;

        public string ID
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