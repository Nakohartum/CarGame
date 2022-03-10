using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Product = UnityEngine.Purchasing.Product;

namespace Game.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonInterstitial;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private IAPButton _buttonBuy;
        [SerializeField] public TMP_Text _text;



        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction showRewarded, 
            UnityAction showIntersitial, UnityAction openShed, UnityAction<Product> Buy)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(openSettings);
            _buttonInterstitial.onClick.AddListener(showIntersitial);
            _buttonRewarded.onClick.AddListener(showRewarded);
            _buttonShed.onClick.AddListener(openShed);
            _buttonBuy.onPurchaseComplete.AddListener(Buy);
            _text.text = "Gold: ";
        }

        public void ChangeText(int gold)
        {
            _text.text = $"Gold: {gold}";
        }
        
        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewarded.onClick.RemoveAllListeners();
            _buttonInterstitial.onClick.RemoveAllListeners();
            _buttonBuy.onPurchaseComplete.RemoveAllListeners();
        }
    }
}
