using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services
{
    [Serializable]
    internal struct Product
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public ProductType ProductType { get; private set; }
    }
}