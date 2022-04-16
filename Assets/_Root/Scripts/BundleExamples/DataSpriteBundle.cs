using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssetBundles
{
    [Serializable]
    internal class DataSpriteBundle : IDataBundle
    {
        public string Name { get; set; }
        public Sprite Icon { get; set; }
    }
}