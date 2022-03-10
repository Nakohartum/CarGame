using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tool
{
    public class CustomText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private TMP_Text _tmpText;


        public string Text
        {
            get => GetText();
            set => SetText(value);
        }

        private void SetText(string value)
        {
            if (_text != null)
            {
                _text.text = value;
            }
            else if (_tmpText.text != null)
            {
                _tmpText.text = value;
            }
        }

        private string GetText()
        {
            if (_text != null)
            {
                return _text.text;
            }
            else if (_tmpText.text != null)
            {
                return _tmpText.text;
            }
            return String.Empty;
        }
    }
}