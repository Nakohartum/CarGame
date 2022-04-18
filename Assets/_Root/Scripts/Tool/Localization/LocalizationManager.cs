using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Localization
{
    internal class LocalizationManager : MonoBehaviour
    {
        private List<LocalizableTextObject> _localizablesStrings = new List<LocalizableTextObject>();
        private TableReference _textTableLocaliztionName = "LocalizationTable";

        public void Start()
        {
            _localizablesStrings = Object.FindObjectsOfType<LocalizableTextObject>().ToList();
            LocalizationSettings.SelectedLocaleChanged += Localize;
        }

        private void Localize(Locale obj)
        {
            StartCoroutine(ChangingLocalizationRoutine());
        }

        private IEnumerator ChangingLocalizationRoutine()
        {
            AsyncOperationHandle<StringTable> loadingTextOperations =
                LocalizationSettings.StringDatabase.GetTableAsync(_textTableLocaliztionName);
            yield return loadingTextOperations;


            if (loadingTextOperations.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError(loadingTextOperations.OperationException);
            }


            
            StringTable table = loadingTextOperations.Result;
            
            for (int i = 0; i < _localizablesStrings.Count; i++)
            {
                _localizablesStrings[i].CustomText.Text = table.GetEntry(_localizablesStrings[i].ID)?.GetLocalizedString();
            }
            
        }
    }
}