using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Rewards.Scripts
{
    internal class RewardView : MonoBehaviour
    {
        private const string CurrencySlotInActiveKey = nameof(CurrencySlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public float TimeCooldown { get; private set; } = 86400;
        [field: SerializeField] public float TimeDeadline { get; private set; } = 172800;
        
        [field: Header("Settings Reward")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }
        
        [field: Header("UI Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotDailyViewPrefab { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotWeeklyViewPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrencySlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrencySlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : (DateTime?) null;
            }
            set
            {
                if (value != null)
                {
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                }
                else
                {
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
                }
            }
        }
        
        
    }
}