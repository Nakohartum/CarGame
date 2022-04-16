using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AssetBundles
{
    internal abstract class AssetBundleViewBase
    {
        public string _urlAssetBundle;

        public AssetBundle _bundle;

        public abstract IEnumerator DownloadAssetBundle();

        public abstract void SetAssetBundle();

        public abstract IEnumerator GetAssetBundle();

        protected void StateRequest(UnityWebRequest request, out AssetBundle bundle)
        {
            if (request.error == null)
            {
                bundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                bundle = null;
                Debug.LogError(request.error);
            }
        }

    }
}