using System;
using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyBoardMove : BaseInputView
    {
        [Header("Settings")] [SerializeField] private float _speed;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void Move()
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            float abs = Mathf.Abs(direction.x);
            float sign = Mathf.Sign(direction.x);
            if (sign > 0)
            {
                OnRightMove(abs);
            }
            else
            {
                OnLeftMove(abs);
            }
        }
    }
}