using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace AssetBundles
{
    internal class MainWindowView : MonoBehaviour
    {
        [Header("Asset Bundles")]
        [SerializeField] private SpriteBundlesDataSet _spriteBundlesDataSet;
        [SerializeField] private Button _downloadButton;
        private SpriteBundleAssetLoader _spriteBundleAssetLoader;
        
        [Header("Addressables")]
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;
        [SerializeField] private AssetReference _backgroundSpritePrefab;
        [SerializeField] private Image _backgroundImage;
        private readonly List<AsyncOperationHandle<Sprite>> _addressablesPrefabs =
            new List<AsyncOperationHandle<Sprite>>();

        private void Start()
        {
            _spriteBundleAssetLoader = new SpriteBundleAssetLoader(_spriteBundlesDataSet);
            _downloadButton.onClick.AddListener(LoadBundles);
            _addBackgroundButton.onClick.AddListener(LoadBackgroundAddresable);
            _removeBackgroundButton.onClick.AddListener(UnloadBackgroundAddressable);
        }

        private void OnDestroy()
        {
            _downloadButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();
        }

        private void LoadBackgroundAddresable()
        {
            AsyncOperationHandle<Sprite> addresablePrefab = Addressables.LoadAssetAsync<Sprite>(_backgroundSpritePrefab);
            addresablePrefab.Completed += SetBackground;
        }

        private void SetBackground(AsyncOperationHandle<Sprite> obj)
        {
            _addressablesPrefabs.Add(obj);
            _backgroundImage.gameObject.SetActive(true);
            _backgroundImage.sprite = obj.Result;
        }

        private void UnloadBackgroundAddressable()
        {
            _backgroundImage.sprite = null;
            _backgroundImage.gameObject.SetActive(false);
            foreach (AsyncOperationHandle<Sprite> handle in _addressablesPrefabs)
            {
                Addressables.Release(handle);
            }
            _addressablesPrefabs.Clear();
        }

        private void LoadBundles()
        {
            _downloadButton.interactable = !_downloadButton.interactable;
            StartCoroutine(LoadImage());
            
        }

        private IEnumerator LoadImage()
        {
            yield return _spriteBundleAssetLoader.DownloadAssetBundle();
            _downloadButton.image.sprite = _spriteBundleAssetLoader.GetIcon(0);
        }
        
    }
}