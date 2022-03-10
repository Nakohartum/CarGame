using Game;
using Profile;
using Services;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameData), menuName = "Configs/"+nameof(GameData))]
internal class GameData : ScriptableObject
{
    [field: SerializeField] public float SpeedCar {get; private set;}
    [field: SerializeField] public float JumpPower {get; private set;}
    [field: SerializeField] public GameState InitialState {get; private set;}
    [field: SerializeField] public TransportType TransportType {get; private set;}
    [field: SerializeField] public Settings Settings {get; private set;}
    [field: SerializeField] public ProductLibrary ProductLibrary {get; private set;}
}