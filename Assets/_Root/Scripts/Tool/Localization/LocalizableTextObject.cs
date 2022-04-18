using UnityEngine;

namespace Tool.Localization
{
    public class LocalizableTextObject : MonoBehaviour, ILocalizable
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public CustomText CustomText { get; set; }
    }
}