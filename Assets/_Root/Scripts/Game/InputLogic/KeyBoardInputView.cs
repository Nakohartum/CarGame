using System;
using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyBoardInputView : BaseInputView
    {
        [Header("Settings")] [SerializeField] private float _speed;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            var direction = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
            float abs = Mathf.Abs(direction);
            float sign = Mathf.Sign(direction);
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