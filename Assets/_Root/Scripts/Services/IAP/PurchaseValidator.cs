using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace Services
{
    internal class PurchaseValidator
    {
        public bool Validate(PurchaseEventArgs args)
        {
            var isValid = true;
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            

            var validator =
                new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
            try
            {
                var result = validator.Validate(args.purchasedProduct.receipt);
            }
            catch (IAPSecurityException)
            {
                isValid = false;
            }
#endif
            return isValid;
        }
    }
}