using System.Threading;
using System.Threading.Tasks;
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
        
        public Task ProcessAsync(IHealth health, CancellationToken token)
        {
            health.TakeDamage(_damage);
            return Task.CompletedTask;
        }
    }
}