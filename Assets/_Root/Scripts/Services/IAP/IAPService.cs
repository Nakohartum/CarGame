using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

namespace Services
{
    internal class IAPService : IIAPService, IStoreListener
    {
        private ProductLibrary _productLibrary;
        public UnityEvent Initialized { get; private set;}
        public UnityEvent PurchaseSucceed { get; private set;}
        public UnityEvent PurchaseFailed { get; private set;}
        public bool IsInitialized { get; private set;}

        private IExtensionProvider _extensionProvider;
        private PurchaseValidator _validator;
        private PurchaseRestorer _restorer;
        private IStoreController _storeController;
        private static IAPService _instance;
        public static IAPService Instance(ProductLibrary productLibrary)
        {
            if (_instance == null)
            {
                _instance = new IAPService(productLibrary);
            }

            return _instance;
        }
        private IAPService(ProductLibrary productLibrary)
        {
            InitializeProducts(productLibrary);
        }

        private void InitializeProducts(ProductLibrary productLibrary)
        {
            StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
            ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(standardPurchasingModule);

            foreach (var product in productLibrary.Products)
            {
                configurationBuilder.AddProduct(product.ID, product.ProductType);
            }

            UnityPurchasing.Initialize(this, configurationBuilder);
        }
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            IsInitialized = true;
            _storeController = controller;
            _extensionProvider = extensions;
            _validator = new PurchaseValidator();
            _restorer = new PurchaseRestorer(_extensionProvider);
            
            Initialized?.Invoke();
        }
        
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            IsInitialized = false;
        }

        public void Buy(string id)
        {
            if (IsInitialized)
            {
                _storeController.InitiatePurchase(id);
            }
            else
            {
                Debug.Log("Error");
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (_validator.Validate(purchaseEvent))
            {
                PurchaseSucceed?.Invoke();
            }
            else
            {
                OnPurchaseFailed(purchaseEvent.purchasedProduct.definition.id, "Non valid");
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(string id, string reason)
        {
            Debug.Log($"{id} buy failed: {reason}");
        }
        

        public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
        {
            OnPurchaseFailed(product.definition.id, failureReason.ToString());
            PurchaseFailed?.Invoke();
        }
        
        public string GetCost(string id)
        {
            UnityEngine.Purchasing.Product product = _storeController.products.WithID(id);
            return product != null ? product.metadata.localizedPriceString : "N/A";
        }

        public void RestorePurchases()
        {
            if (IsInitialized)
            {
                _restorer.Restore();
            }
            else
            {
                Debug.Log("Error");
            }
        }
        
    }
}