using Factura.Gameplay.Health;

namespace Factura.Gameplay.Attack
{
    public sealed class ImmediateAttackComponent : IAttack
    {
        private readonly int _damage;

        public ImmediateAttackComponent(int damage)
        {
            _damage = damage;
        }

        public void Process(IHealth health)
        {
            health.TakeDamage(_damage);
        }
    }
}