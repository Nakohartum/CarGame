using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Rewards.Scripts
{
    internal class CurrencyView : MonoBehaviour
    {
        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currencyDiamond;

        public void Init(int woodCount, int diamondCount)
        {
            SetWood(woodCount);
            SetDiamond(diamondCount);
        }

        public void SetDiamond(int diamondCount)
        {
            _currencyDiamond.SetData(diamondCount);
        }

        public void SetWood(int woodCount)
        {
            _currencyWood.SetData(woodCount);
        }
    }
}