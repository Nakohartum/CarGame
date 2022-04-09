using Tool;
using UnityEngine;

namespace _Rewards.Scripts
{
    internal class CurrencyController : BaseController
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);

        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Rewards/CurrencyView");
        private readonly CurrencyView _currencyView;
        
        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }
        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }

        public CurrencyController(Transform placeForUI)
        {
            _currencyView = LoadView(placeForUI);
            RefreshView();
        }
        
        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }
        
        public void AddWood(int value)
        {
            Wood += value;
            _currencyView.SetWood(Wood);
        }
        public void AddDiamond(int value)
        {
            Diamond += value;
            _currencyView.SetDiamond(Diamond);
        }
        
        public void ResetWood()
        {
            Wood = 0;
            _currencyView.SetWood(Wood);
        }
        public void ResetDiamond()
        {
            Diamond = 0;
            _currencyView.SetDiamond(Diamond);
        }

        public void RefreshView()
        {
            _currencyView.Init(Wood, Diamond);
        }
    }
}