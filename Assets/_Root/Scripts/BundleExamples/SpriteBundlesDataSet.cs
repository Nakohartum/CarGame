using UnityEngine;

namespace AssetBundles
{
    [CreateAssetMenu(fileName = nameof(SpriteBundlesDataSet), menuName = "Configs/"+nameof(SpriteBundlesDataSet))]
    internal class SpriteBundlesDataSet : ScriptableObject
    {
        public string[] Names;
    }
}