using System;

namespace Factura.Gameplay.Health
{
    public interface IHealth
    {
        event Action OnDie;
        void TakeDamage(int amount);
    }
}