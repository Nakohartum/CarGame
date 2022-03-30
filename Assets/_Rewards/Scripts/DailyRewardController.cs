using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Rewards.Scripts
{
    internal class DailyRewardController
    {
        private readonly DailyRewardView _view;

        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;

        public DailyRewardController(DailyRewardView view)
        {
            _view = view;
        }

        public void Init()
        {
            if (_isInitialized)
            {
                return;
            }

            InitSlots();
            RefreshUI();
            StartRewardUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }

        public void DeInit()
        {
            if (!_isInitialized)
            {
                return;
            }

            DeInitSlots();
            StopRewardUpdating();
            UnSubscribeButtons();

            _isInitialized = false;
        }

        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _view.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView(_view.Rewards[i]);
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView(Reward reward)
        {
            switch (reward.RewardDayType)
            {
                case RewardDayType.Daily:
                    return Object.Instantiate(_view.ContainerSlotDailyViewPrefab, _view.MountRootSlotsReward, false);
                case RewardDayType.Weekly:
                    return Object.Instantiate(_view.ContainerSlotWeeklyViewPrefab, _view.MountRootSlotsReward, false);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void DeInitSlots()
        {
            foreach (var slot in _slots)
            {
                Object.Destroy(slot);
            }
            _slots.Clear();
        }

        private void StartRewardUpdating()
        {
            _coroutine = _view.StartCoroutine(RewardsStateUpdater());
        }

        private void StopRewardUpdating()
        {
            if (_coroutine == null)
            {
                return;
            }
            _view.StopCoroutine(RewardsStateUpdater());
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);
            while (true)
            {
                RefreshRewardsState();
                RefreshUI();
                yield return waitForSeconds;
            }
        }

        private void SubscribeButtons()
        {
            _view.GetRewardButton.onClick.AddListener(ClaimReward);
            _view.ResetButton.onClick.AddListener(ResetRewardsState);
        }

        private void UnSubscribeButtons()
        {
            _view.GetRewardButton.onClick.RemoveAllListeners();
            _view.ResetButton.onClick.RemoveAllListeners();
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
            {
                return;
            }

            Reward reward = _view.Rewards[_view.CurrentSlotInActive];

            switch (reward.RewardResourceType)
            {
                case RewardResourceType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    break;
                case RewardResourceType.Diamond:
                    CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                    break;
            }
            _view.TimeGetReward = DateTime.UtcNow;
            _view.CurrentSlotInActive++;
            RefreshRewardsState();
        }

        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _view.TimeGetReward.HasValue;

            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting = DateTime.UtcNow - _view.TimeGetReward.Value;

            bool isDeadLineElapsed = timeFromLastRewardGetting.Seconds >= _view.TimeDeadline;
            bool isTimeToGetNewReward = timeFromLastRewardGetting.Seconds >= _view.TimeCooldown;

            if (isDeadLineElapsed)
            {
                ResetRewardsState();
            }

            _isGetReward = isTimeToGetNewReward;
        }

        private void ResetRewardsState()
        {
            _view.TimeGetReward = null;
            _view.CurrentSlotInActive = 0;
            CurrencyView.Instance.ResetWood();
            CurrencyView.Instance.ResetDiamond();
        }

        private void RefreshUI()
        {
            _view.GetRewardButton.interactable = _isGetReward;
            _view.TimerNewReward.text = GetTimerNewReward();
            RefreshSlots();
        }

        private string GetTimerNewReward()
        {
            if (_isGetReward)
            {
                return "The reward is ready to be received";
            }

            if (_view.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _view.TimeGetReward.Value.AddSeconds(_view.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                       $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Reward reward = _view.Rewards[i];
                int countDay = i + 1;
                bool isSelected = i == _view.CurrentSlotInActive;
                _slots[i].SetData(reward, countDay, isSelected);
            }
        }
    }
}