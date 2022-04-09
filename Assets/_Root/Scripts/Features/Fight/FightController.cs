using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController : BaseController
    {
        private ProfilePlayer _profilePlayer;
        private ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly FightView _fightView;
        private readonly Enemy _enemy;
        
        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimePlayer;

        private DataPlayer _money;
        private DataPlayer _health;
        private DataPlayer _power;
        private DataPlayer _crime;


        public FightController(ProfilePlayer profilePlayer, Transform placeForUI)
        {
            _profilePlayer = profilePlayer;
            _fightView = LoadView(placeForUI);
            _enemy = new Enemy("Enemy");

            _money = CreateDataPlayer(DataType.Money);
            _health = CreateDataPlayer(DataType.Health);
            _power = CreateDataPlayer(DataType.Power);
            _crime = CreateDataPlayer(DataType.Crime);
            Subscribe(_fightView);
        }

        private FightView LoadView(Transform placeForUI)
        {
            
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI);
            AddGameObject(objectView);
            return objectView.GetComponent<FightView>();
        }


        protected override void OnDispose()
        {
            base.OnDispose();
            DisposeDataPlayer(ref _money);
            DisposeDataPlayer(ref _health);
            DisposeDataPlayer(ref _power);
            DisposeDataPlayer(ref _crime);
            Unsubscribe(_fightView);
        }

        private DataPlayer CreateDataPlayer(DataType type)
        {
            DataPlayer dataPlayer = new DataPlayer(type);
            dataPlayer.Attach(_enemy);
            return dataPlayer;
        }

        private void DisposeDataPlayer(ref DataPlayer dataPlayer)
        {
            dataPlayer.Dettach(_enemy);
            dataPlayer = null;
        }

        private void Subscribe(FightView fightView)
        {
            fightView.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            fightView.MinusMoneyButton.onClick.AddListener(DecreaseMoney);
            fightView.AddHealthButton.onClick.AddListener(IncreaseHealth);
            fightView.MinusHealthButton.onClick.AddListener(DecreaseHealth);
            fightView.AddPowerButton.onClick.AddListener(IncreasePower);
            fightView.MinusPowerButton.onClick.AddListener(DecreasePower);
            fightView.AddCrimeButton.onClick.AddListener(IncreaseCrime);
            fightView.MinusCrimeButton.onClick.AddListener(DecreaseCrime);
            
            fightView.LeaveButton.onClick.AddListener(Leave);
            fightView.FightButton.onClick.AddListener(Fight);
        }

        

        private void Unsubscribe(FightView fightView)
        {
            fightView.AddMoneyButton.onClick.RemoveAllListeners();
            fightView.MinusMoneyButton.onClick.RemoveAllListeners();
            fightView.AddHealthButton.onClick.RemoveAllListeners();
            fightView.MinusHealthButton.onClick.RemoveAllListeners();
            fightView.AddPowerButton.onClick.RemoveAllListeners();
            fightView.MinusPowerButton.onClick.RemoveAllListeners();
            fightView.AddCrimeButton.onClick.RemoveAllListeners();
            fightView.MinusCrimeButton.onClick.RemoveAllListeners();
            
            fightView.LeaveButton.onClick.RemoveAllListeners();
            fightView.FightButton.onClick.RemoveAllListeners();
        }

        private void IncreaseMoney() => IncreaseValue(ref _allCountMoneyPlayer, DataType.Money);
        private void DecreaseMoney() => DecreaseValue(ref _allCountMoneyPlayer, DataType.Money);
        
        private void IncreaseHealth() => IncreaseValue(ref _allCountHealthPlayer, DataType.Health);
        private void DecreaseHealth() => DecreaseValue(ref _allCountHealthPlayer, DataType.Health);
        
        private void IncreasePower() => IncreaseValue(ref _allCountPowerPlayer, DataType.Power);
        private void DecreasePower() => DecreaseValue(ref _allCountPowerPlayer, DataType.Power);
        
        private void IncreaseCrime()
        {
            IncreaseValue(ref _allCountCrimePlayer, DataType.Crime);
            UpdateEscapeButtonVisibility();
        }

        private void DecreaseCrime()
        {
            if (_allCountCrimePlayer == 0)
            {
                return;
            }
            DecreaseValue(ref _allCountCrimePlayer, DataType.Crime);
            UpdateEscapeButtonVisibility();
        }
        
        
        private void IncreaseValue(ref int value, DataType type)
        {
            AddToValue(ref value, 1, type);
        }

        private void DecreaseValue(ref int value, DataType type)
        {
            AddToValue(ref value, -1, type);
        }

        private void AddToValue(ref int value, int addition, DataType dataType)
        {
            value += addition;
            ChangeDataWindow(value, dataType);
        }

        private void ChangeDataWindow(int value, DataType dataType)
        {
            DataPlayer dataPlayer = GetDataPlayer(dataType);
            TMP_Text textComponent = GetText(dataType, _fightView);
            string text = $"Player {dataType:F} {value}";

            dataPlayer.Value = value;
            textComponent.text = text;

            int enemyPower = _enemy.CalcPower();
            _fightView.CountPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private TMP_Text GetText(DataType dataType, FightView fightView)
        {
            switch (dataType)
            {
                case DataType.Health:
                    return fightView.CountHealthText;
                case DataType.Money:
                    return fightView.CountMoneyText;
                case DataType.Power:
                    return fightView.CountPowerText;
                case DataType.Crime:
                    return fightView.CountCrimeText;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private DataPlayer GetDataPlayer(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Health:
                    return _health;
                case DataType.Money:
                    return _money;
                case DataType.Power:
                    return _power;
                case DataType.Crime:
                    return _crime;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
        private void UpdateEscapeButtonVisibility()
        {
            const int minCrimeToUse = 0;
            const int maxCrimeToUse = 2;
            const int minCrimeToShow = 0;
            const int maxCrimeToShow = 5;

            bool canUse = minCrimeToUse <= _allCountCrimePlayer && _allCountCrimePlayer <= maxCrimeToUse;
            bool canShow = minCrimeToShow <= _allCountCrimePlayer && _allCountCrimePlayer <= maxCrimeToShow;

            _fightView.LeaveButton.interactable = canUse;
            _fightView.LeaveButton.gameObject.SetActive(canShow);
        }
        
        private void Leave()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }
        
        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _allCountPowerPlayer > enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";
            
            Debug.Log($"<color={color}> {message}!!! </color>");
        }
    }
}