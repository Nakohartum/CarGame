using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ability
{
    internal interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void Deinit();
    }
    internal class AbilityButtonView : MonoBehaviour, IAbilityButtonView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        private void OnDestroy()
        {
            Deinit();
        }

        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            _button.onClick.AddListener(click);
        }

        public void Deinit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}