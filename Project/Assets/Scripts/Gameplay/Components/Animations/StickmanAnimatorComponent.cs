using UnityEngine;

namespace Factura.Gameplay.Animations
{
    public sealed class StickmanAnimatorComponent : IStickmanAnimator
    {
        private static readonly int IsPatrolling = Animator.StringToHash("IsPatrolling");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsChasing = Animator.StringToHash("IsChasing");

        private readonly Animator _source;

        public StickmanAnimatorComponent(Animator source)
        {
            _source = source;
        }

        public void PlayPatrol(bool value) => _source.SetBool(IsPatrolling, value);
        public void PlayAttack() => _source.SetTrigger(Attack);
        public void PlayChase(bool value) => _source.SetBool(IsChasing, value);

        public void PlayIdle()
        {
            _source.SetBool(IsPatrolling, false);
            _source.SetBool(IsChasing, false);
        }
    }
}