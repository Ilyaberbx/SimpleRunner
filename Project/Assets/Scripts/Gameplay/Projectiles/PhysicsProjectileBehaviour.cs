using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PhysicsProjectileBehaviour : BaseProjectileBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _aliveTime;

        private Vector3 _direction;

        public override void Initialize(Vector3 direction)
        {
            _direction = direction;
            ApplyForce();
            SelfDestroy();
        }

        private void ApplyForce()
        {
            var force = _direction * _moveSpeed;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void SelfDestroy()
        {
            Destroy(gameObject, _aliveTime);
        }
    }
}