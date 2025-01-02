using System;

namespace Factura.Gameplay.Damage
{
    public interface IDamageable
    {
        event Action OnDie;
        void TakeDamage(int amount);
    }
}