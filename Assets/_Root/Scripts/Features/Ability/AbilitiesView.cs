using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Ability
{
    internal interface IAbilitiesView
    {
        void Display(IEnumerable<IAbilityItemConfig> abilityItems, Action<string> clicked);
        void Clear();
        GameObject GameObject { get; }
    }
    internal class AbilitiesView : MonoBehaviour, IAbilitiesView
    {
        [SerializeField] private GameObject _abilityButtonPrefab;
        [SerializeField] private Transform _placeForButtons;
        public GameObject GameObject
        {
            get => gameObject;
        }

        private readonly Dictionary<string, AbilityButtonView> _buttonViews =
            new Dictionary<string, AbilityButtonView>();

        private void OnDestroy()
        {
            Clear();
        }

        public void Display(IEnumerable<IAbilityItemConfig> abilityItems, Action<string> clicked)
        {
            Clear();

            foreach (var itemConfig in abilityItems)
            {
                _buttonViews[itemConfig.ID] = CreateButtonView(itemConfig, clicked);
            }
        }

        private AbilityButtonView CreateButtonView(IAbilityItemConfig itemConfig, Action<string> clicked)
        {
            GameObject objectView = Instantiate(_abilityButtonPrefab, _placeForButtons, false);
            AbilityButtonView abilityButtonView = objectView.GetComponent<AbilityButtonView>();
            abilityButtonView.Init(itemConfig.Icon, () => { clicked?.Invoke(itemConfig.ID);});
            return abilityButtonView;
        }

        public void Clear()
        {
            foreach (var buttonView in _buttonViews.Values)
            {
                DestroyButtonView(buttonView);
            }
            _buttonViews.Clear();
        }

        

        private void DestroyButtonView(AbilityButtonView buttonView)
        {
            buttonView.Deinit();
            Destroy(buttonView.gameObject);
        }
    }
}