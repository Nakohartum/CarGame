using System;
using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyBoardInputView : BaseInputView
    {
        

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            var direction = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
            var isJumping = Input.GetAxis("Vertical") * _jumpPower;
            float abs = Mathf.Abs(direction);
            float sign = Mathf.Sign(direction);
            if (isJumping > 0.1f)
            {
                OnJumpMove(isJumping);
            }
            if (sign > 0)
            {
                OnRightMove(abs);
            }
            else
            {
                OnLeftMove(abs);
            }
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
        }
    }
}