using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Configs/Inventory/"+nameof(ItemConfig), order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}