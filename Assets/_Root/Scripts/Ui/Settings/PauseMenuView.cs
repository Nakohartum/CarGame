using Tween;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Settings
{
    internal class PauseMenuView : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] public Button ButtonPause { get; private set; }
        [field: SerializeField] public PopupView Panel { get; private set; }
    }
}