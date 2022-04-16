using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace AssetBundles
{
    internal class SpriteBundleAssetLoader : AssetBundleViewBase
    {
        private List<DataSpriteBundle> _dataSpriteBundle = new List<DataSpriteBundle>();

        public SpriteBundleAssetLoader(SpriteBundlesDataSet spriteBundlesDataSet)
        {
            for (int i = 0; i < spriteBundlesDataSet.Names.Length; i++)
            {
                _dataSpriteBundle.Add(
                    new DataSpriteBundle
                    {
                        Name = spriteBundlesDataSet.Names[i]
                    }
                );
            }
        }

        public Sprite GetIcon(int index)
        {
            return _dataSpriteBundle[index].Icon;
        }

        public override IEnumerator DownloadAssetBundle()
        {
            yield return GetAssetBundle();

            if (_dataSpriteBundle != null)
            {
                SetAssetBundle();
            }
            else
            {
                Debug.LogError($"{nameof(_dataSpriteBundle)} is not loaded");
            }
        }

        public override IEnumerator GetAssetBundle()
        {
            _urlAssetBundle = "https://drive.google.com/uc?export=download&id=1wvhemdm3Dm5QYDwfkvFKxlcNAmydVI18";
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_urlAssetBundle);
            yield return request.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }

            StateRequest(request, out _bundle);
        }

        public override void SetAssetBundle()
        {
            foreach (var data in _dataSpriteBundle)
            {
                data.Icon = _bundle.LoadAsset<Sprite>(data.Name);
            }
        }
        
    }
}