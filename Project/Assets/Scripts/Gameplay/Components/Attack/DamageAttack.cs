using Factura.Gameplay.Health;

namespace Factura.Gameplay.Attack
{
    public sealed class DamageAttack : IAttack
    {
        private readonly int _damage;

        public DamageAttack(int damage)
        {
            _damage = damage;
        }

        public void Process(IHealth health)
        {
            health.TakeDamage(_damage);
        }
    }
}