using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour
    {
        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;
        private SubscriptionProperty<float> _jumpMove;
        protected float _speed;
        protected float _jumpPower;
        protected ContactPoller _contactPoller;
        protected Rigidbody2D _rigidbody;

        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            SubscriptionProperty<float> jumpMove,
            float speed, float jumpPower, TransportController transportController)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            _jumpMove = jumpMove;
            _speed = speed;
            _jumpPower = jumpPower;
            _contactPoller = new ContactPoller(transportController);
            _rigidbody = transportController.ViewGameObject.GetComponent<Rigidbody2D>();
        }

        protected virtual void OnLeftMove(float value)
        {
            _leftMove.Value = value;
        }


        protected virtual void OnRightMove(float value)
        {
            _rightMove.Value = value;
        }

        protected virtual void OnJumpMove(float value)
        {
            if (_contactPoller.IsGrounded)
            {
                _jumpMove.Value = value;
                _rigidbody.AddForce(Vector2.up * _jumpMove.Value, ForceMode2D.Impulse);
            }
        }
    }
}
