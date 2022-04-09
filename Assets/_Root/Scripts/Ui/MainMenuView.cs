using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Product = UnityEngine.Purchasing.Product;

namespace Game.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAd;
        [SerializeField] private Button _buttonInterstitial;
        [SerializeField] private Button _buttonShed;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private IAPButton _buttonBuy;
        [SerializeField] public TMP_Text _text;



        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction showRewarded, 
            UnityAction showIntersitial, UnityAction openShed, UnityAction<Product> Buy, UnityAction reward)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(openSettings);
            _buttonInterstitial.onClick.AddListener(showIntersitial);
            _buttonRewardedAd.onClick.AddListener(showRewarded);
            _buttonShed.onClick.AddListener(openShed);
            _buttonReward.onClick.AddListener(reward);
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
            _buttonRewardedAd.onClick.RemoveAllListeners();
            _buttonInterstitial.onClick.RemoveAllListeners();
            _buttonBuy.onPurchaseComplete.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
        }
    }
}
