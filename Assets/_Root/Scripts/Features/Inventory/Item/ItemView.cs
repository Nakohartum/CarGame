using System;
using Tool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Inventory
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clickAction);
    }
    internal class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private CustomText _text;
        [SerializeField] private Button _button;

        [SerializeField] private GameObject _selectedBackground;
        [SerializeField] private GameObject _unSelectedBackground;

        public void Init(IItem item, UnityAction clickAction)
        {
            _text.Text = item.ItemInfo.Title;
            _icon.sprite = item.ItemInfo.Icon;
            _button.onClick.AddListener(clickAction);
        }

        public void Select() => SetSelected(true);

        public void Unselect() => SetSelected(false);

        private void SetSelected(bool isSelected)
        {
            _selectedBackground.SetActive(isSelected);
            _unSelectedBackground.SetActive(!isSelected);
        }
        
        private void OnDestroy()
        {
            Deinit();
        }

        public void Deinit()
        {
            _text.Text = String.Empty;
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }
    }
}