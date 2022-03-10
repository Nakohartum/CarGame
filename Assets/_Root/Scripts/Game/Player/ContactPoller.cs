using JoostenProductions;
using UnityEngine;

namespace Game
{
    internal class ContactPoller : BaseController
    {
        private const float _collisionThresh = 0.5f;
        private ContactPoint2D[] _contactPoints = new ContactPoint2D[10];
        private int _countContacts;
        private Collider2D _collider;
        
        public bool IsGrounded { get; private set; }
        
        
        public ContactPoller(TransportController transportController)
        {
            _collider = transportController.ViewGameObject.GetComponent<Collider2D>();
            UpdateManager.SubscribeToUpdate(UpdateContatcs);
        }

        private void UpdateContatcs()
        {
            IsGrounded = false;
            _countContacts = _collider.GetContacts(_contactPoints);

            for (int i = 0; i < _countContacts; i++)
            {
                var normal = _contactPoints[i].normal;
                var rigidbody = _contactPoints[i].rigidbody;
                if (normal.y > _collisionThresh)
                {
                    IsGrounded = true;
                }
            }
        }

        protected override void OnDispose()
        {
            UpdateManager.UnsubscribeFromUpdate(UpdateContatcs);
            base.OnDispose();
        }
    }
}