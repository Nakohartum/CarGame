using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skipped;
        public event Action BecomeReady;
        protected readonly string _id;

        protected UnityAdsPlayer(string id)
        {
            _id = id;
            Advertisement.AddListener(this);
        }
        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void OnPlaying();
        protected abstract void Load();
        
        public void OnUnityAdsReady(string placementId)
        {
            if (_id != placementId)
            {
                return;
            }
            BecomeReady?.Invoke();
        }

        public void OnUnityAdsDidError(string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            if (_id != placementId)
            {
                return;
            }
            Started?.Invoke();
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (_id != placementId)
            {
                return;
            }

            switch (showResult)
            {
                case ShowResult.Failed:
                    Failed?.Invoke();
                    break;
                case ShowResult.Skipped:
                    Skipped?.Invoke();
                    break;
                case ShowResult.Finished:
                    Finished?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
        }
    }
}