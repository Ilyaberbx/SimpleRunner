using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Factura.Gameplay.Enemy.States
{
    public sealed class EnemyDeadState : BaseEnemyState
    {
        private readonly GameObject _source;

        public EnemyDeadState(GameObject source)
        {
            _source = source;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            _source.SetActive(false);
            return Task.CompletedTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}