using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = nameof(ProductLibrary), menuName = "Configs/"+nameof(ProductLibrary), order = 0)]
    internal class ProductLibrary : ScriptableObject
    {
        [field: SerializeField] public Product[] Products { get; private set; }
    }
}