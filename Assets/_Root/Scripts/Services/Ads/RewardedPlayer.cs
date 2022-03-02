using UnityEngine.Advertisements;

namespace Services
{
    internal class RewardedPlayer : UnityAdsPlayer
    {
        public RewardedPlayer(string id) : base(id)
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