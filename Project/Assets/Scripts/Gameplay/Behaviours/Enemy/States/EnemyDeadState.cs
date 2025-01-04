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

        protected override void Enter()
        {
            Object.Destroy(_source);
        }

        protected override void Exit()
        {
        }
    }
}