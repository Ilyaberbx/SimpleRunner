using System;
using UnityEngine;

namespace Factura.Gameplay.Triggers
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseTriggerObserver<TComponent> : MonoBehaviour
    {
        public event Action<TComponent> OnEnter;
        public event Action<TComponent> OnExit;
        public event Action<TComponent> OnStay;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnEnter?.Invoke(component);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnStay?.Invoke(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<TComponent>(out var component))
            {
                OnExit?.Invoke(component);
            }
        }
    }
}