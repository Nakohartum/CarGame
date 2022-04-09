using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Fight
{
    internal class StartFightView : MonoBehaviour
    {
        [SerializeField] private Button _startFightButton;

        public void Init(UnityAction fight)
        {
            _startFightButton.onClick.AddListener(fight);
        }

        private void OnDestroy()
        {
            _startFightButton.onClick.RemoveAllListeners();
        }
    }
}