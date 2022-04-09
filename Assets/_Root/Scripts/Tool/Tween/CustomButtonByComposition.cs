using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private Vector3 _endValue;

        private Tweener _currentTween = null;

        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveAllListeners();

        private void InitComponents()
        {
            _button ??= GetComponent<Button>();
            _rectTransform ??= GetComponent<RectTransform>();
        }


        private void OnButtonClick() =>
            ActivateAnimation();

        [ContextMenu("Start animation")]
        private void ActivateAnimation()
        {
            if (_currentTween != null)
            {
                return;
            }
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _currentTween = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase)
                        .OnComplete(() => _currentTween = null);
                    break;

                case AnimationButtonType.ChangePosition:
                    _currentTween = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase)
                        .OnComplete(() => _currentTween = null);
                    break;
                case AnimationButtonType.ChangeScale:
                    PlayChangeScaleAnimation();
                    break;
            }
        }

        private void PlayChangeScaleAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            _currentTween = _rectTransform.DOScale(_endValue, _duration / 2);
            var normalValue = _rectTransform.localScale;
            sequence.Append(_currentTween);
            _currentTween = _rectTransform.DOScale(normalValue, _duration/2);
            sequence.Append(_currentTween).OnComplete(() => _currentTween = null);
        }

        [ContextMenu("Stop animation")]
        private void StopAnimation()
        {
            _currentTween.Kill();
            _currentTween = null;
        }
    }
}
