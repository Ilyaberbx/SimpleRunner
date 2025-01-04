using System;
using UnityEngine;

namespace Factura.Gameplay.Health
{
    public sealed class HealthComponent : IHealth
    {
        private int _health;
        public event Action OnDie;

        public HealthComponent(int health)
        {
            _health = health;
        }

        public void TakeDamage(int amount)
        {
            if (_health == 0)
            {
                return;
            }

            _health = Mathf.Clamp(_health - amount, 0, _health);

            if (_health <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}