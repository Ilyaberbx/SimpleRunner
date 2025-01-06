using System;
using UnityEngine;

namespace Factura.Gameplay.Enemy.Stickman
{
    [RequireComponent(typeof(Animator))]
    public class StickmanAnimationEventsObserver : MonoBehaviour
    {
        public event Action OnAttack;

        private void OnAttacked()
        {
            OnAttack?.Invoke();
        }
    }
}