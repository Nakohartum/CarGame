using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services
{
    internal class PurchaseRestorer
    {
        private readonly IExtensionProvider _extensionProvider;

        public PurchaseRestorer(IExtensionProvider extensionProvider)
        {
            _extensionProvider = extensionProvider;
        }

        public void Restore()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    _extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnTransactionRestored);
                    break;
                case RuntimePlatform.Android:
                    _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnTransactionRestored);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTransactionRestored(bool obj)
        {
            if (obj)
            {
                Debug.Log("Restored");
            }
        }
    }
}