using System;
using UnityEngine;

namespace AssetBundles
{
    [Serializable]
    internal class DataBundleAsset : IDataBundle
    {
        public string Name { get; set; }
        public Sprite Icon { get; set; }
    }
}