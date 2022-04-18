using System.Collections.Generic;
using UnityEngine;

namespace Tool.Localization
{
    [CreateAssetMenu(fileName = nameof(LocalizableObjects), menuName = "Configs/Localization/"+nameof(LocalizableObjects), order = 0)]
    internal class LocalizableObjects : ScriptableObject
    {
        [field: SerializeField] public List<string> Localizables { get; private set; }
    }
}