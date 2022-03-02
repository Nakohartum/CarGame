using UnityEngine.Advertisements;

namespace Services
{
    internal class InterstitionalPlayer : UnityAdsPlayer
    {
        public InterstitionalPlayer(string id) : base(id)
        {
        }

        protected override void OnPlaying()
        {
            Advertisement.Show(_id);
        }

        protected override void Load()
        {
            Advertisement.Load(_id);
        }
    }
}