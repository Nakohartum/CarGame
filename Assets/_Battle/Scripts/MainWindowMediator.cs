using System;
using System.Collections;
using System.Collections.Generic;
using _Battle.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace _Battle
{
    public class MainWindowMediator : MonoBehaviour
{
    [Header("Player Stats")] 
    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    [SerializeField] private TMP_Text _countPowerText;
    [SerializeField] private TMP_Text _countCrimeText;

    [Header("Enemy Stats")] 
    [SerializeField] private TMP_Text _countPowerEnemyText;
    [SerializeField] private int _minCrime;
    [SerializeField] private int _maxCrime;

    [Header("Money Buttons")] 
    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;

    [Header("Health Buttons")] 
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;

    [Header("Power Buttons")] 
    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;
    
    [Header("Crime Buttons")] 
    [SerializeField] private Button _addCrimeButton;
    [SerializeField] private Button _minusCrimeButton;

    [Header("Other buttons")] 
    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _leaveButton;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountCrimePlayer;

    private DataPlayer _money;
    private DataPlayer _health;
    private DataPlayer _power;
    private DataPlayer _crime;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = new Enemy("Enemy");

        _money = CreateDataPlayer(DataType.Money);
        _health = CreateDataPlayer(DataType.Health);
        _power = CreateDataPlayer(DataType.Power);
        _crime = CreateDataPlayer(DataType.Crime);
        Subscribe();
    }

    private void OnDestroy()
    {
        DisposeDataPlayer(ref _money);
        DisposeDataPlayer(ref _health);
        DisposeDataPlayer(ref _power);
        Unsubscribe();
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

    private void Subscribe()
    {
        _addMoneyButton.onClick.AddListener(IncreaseMoney);
        _minusMoneyButton.onClick.AddListener(DecreaseMoney);
        _addHealthButton.onClick.AddListener(IncreaseHealth);
        _minusHealthButton.onClick.AddListener(DecreaseHealth);
        _addPowerButton.onClick.AddListener(IncreasePower);
        _minusPowerButton.onClick.AddListener(DecreasePower);
        _addCrimeButton.onClick.AddListener(IncreaseCrime);
        _minusCrimeButton.onClick.AddListener(DecreaseCrime);
        
        _leaveButton.onClick.AddListener(Leave);
        _fightButton.onClick.AddListener(Fight);
    }

    

    private void Unsubscribe()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();
        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();
        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();
        _addCrimeButton.onClick.RemoveAllListeners();
        _minusCrimeButton.onClick.RemoveAllListeners();
        
        _leaveButton.onClick.RemoveAllListeners();
        _fightButton.onClick.RemoveAllListeners();
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
        CheckLeaveStatus(_allCountCrimePlayer);
    }

    private void DecreaseCrime()
    {
        DecreaseValue(ref _allCountCrimePlayer, DataType.Crime);
        CheckLeaveStatus(_allCountCrimePlayer);
    }

    private void CheckLeaveStatus(int value)
    {
        if (value > _minCrime)
        {
            _leaveButton.gameObject.SetActive(false);
        }
        else
        {
            _leaveButton.gameObject.SetActive(true);
        }
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
        TMP_Text textComponent = GetText(dataType);
        string text = $"Player {dataType:F} {value}";

        dataPlayer.Value = value;
        textComponent.text = text;

        int enemyPower = _enemy.CalcPower();
        _countPowerEnemyText.text = $"Enemy Power: {enemyPower}";
    }

    private TMP_Text GetText(DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                return _countHealthText;
            case DataType.Money:
                return _countMoneyText;
            case DataType.Power:
                return _countPowerText;
            case DataType.Crime:
                return _countCrimeText;
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

    private void Leave()
    {
        Debug.Log($"Left!");
    }
    
    private void Fight()
    {
        int enemyPower = _enemy.CalcPower();
        bool isVictory = _allCountPowerPlayer > enemyPower;

        string color = isVictory ? "#07FF00" : "#FF0000";
        string message = isVictory ? "Win" : "Lose";
        
        Debug.Log($"<color = {color}> {message}!!! </color>");
    }
}
}



