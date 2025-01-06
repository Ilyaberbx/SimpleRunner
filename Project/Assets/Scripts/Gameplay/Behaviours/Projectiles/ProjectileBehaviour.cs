using Better.Locators.Runtime;
using Factura.Gameplay.Enemy.Stickman;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Update;
using Factura.Gameplay.Triggers;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class ProjectileBehaviour : BaseProjectileBehaviour, IGameUpdatable
    {
        [SerializeField] private ProjectileVisitorTriggerObserver _visitorTriggerObserver;
        private int _damage;
        private float _moveSpeed;
        private float _lifetime;

        private LevelService _levelService;
        private GameUpdateService _gameUpdateService;

        private Transform _cachedTransform;
        private Vector3 _direction;

        public int Damage => _damage;

        public void Initialize(int damage, float moveSpeed, float lifeTime)
        {
            _damage = damage;
            _moveSpeed = moveSpeed;
            _lifetime = lifeTime;

            _cachedTransform = transform;
            _levelService = ServiceLocator.Get<LevelService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();
        }

        public override void Fire(Vector3 direction)
        {
            _direction = direction;
            DestroyAfterLifetime();
            _levelService.OnLevelFinish += OnLevelFinished;
            _visitorTriggerObserver.OnEnter += OnVisitorEntered;

            _gameUpdateService.Subscribe(this);
        }

        private void OnDestroy()
        {
            _levelService.OnLevelFinish -= OnLevelFinished;
            _visitorTriggerObserver.OnEnter -= OnVisitorEntered;

            _gameUpdateService.Unsubscribe(this);
        }

        public void OnUpdate(float deltaTime)
        {
            var movement = ProcessMovement(deltaTime);
            _cachedTransform.position += movement;
        }

        private Vector3 ProcessMovement(float deltaTime)
        {
            return _direction * _moveSpeed * deltaTime;
        }

        public void Visit(StickmanBehaviour stickman)
        {
            stickman.Health.TakeDamage(Damage);
            DestroyImmediately();
        }

        private void OnLevelFinished()
        {
            DestroyImmediately();
        }

        private void DestroyAfterLifetime()
        {
            Destroy(gameObject, _lifetime);
        }

        private void DestroyImmediately()
        {
            Destroy(gameObject);
        }

        private void OnVisitorEntered(IProjectileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}