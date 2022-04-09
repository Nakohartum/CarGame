using System;
using UnityEngine;

namespace _Rewards.Scripts
{
    [Serializable]
    internal class Reward
    {
        [field:SerializeField] public RewardResourceType RewardResourceType { get; private set; }
        [field:SerializeField] public RewardDayType RewardDayType { get; private set; }
        [field:SerializeField] public Sprite IconCurrency { get; private set; }
        [field:SerializeField] public int CountCurrency { get; private set; }
    }
}