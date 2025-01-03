using System;
using Better.Locators.Runtime;
using Factura.Gameplay.Services.Level;
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
        private LevelService _levelService;

        public override void Initialize(Vector3 direction)
        {
            _direction = direction;
            ApplyForce();
            SelfDestroy();

            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelFinish += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        private void ApplyForce()
        {
            var force = _direction * _moveSpeed;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void OnLevelFinished()
        {
            DestroyImmediately();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject, _aliveTime);
        }

        private void DestroyImmediately()
        {
            Destroy(gameObject);
        }
    }
}