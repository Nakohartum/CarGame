using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;


        public void Init(UnityAction backAction)
        {
            _buttonBack.onClick.AddListener(backAction);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}