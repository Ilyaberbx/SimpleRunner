using Better.Locators.Runtime;
using Factura.Gameplay.Enemy;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Services.Update;
using Factura.Gameplay.Triggers;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class ProjectileBehaviour : BaseProjectileBehaviour, IProjectileVisitor, IGameUpdatable
    {
        [SerializeField] private ProjectileVisitableTriggerObserver _visitableTriggerObserver;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _aliveTime;

        private LevelService _levelService;
        private GameUpdateService _gameUpdateService;

        private Transform _cachedTransform;
        private Vector3 _direction;

        public override void Initialize(Vector3 direction)
        {
            _cachedTransform = transform;
            _direction = direction;
            _levelService = ServiceLocator.Get<LevelService>();
            _gameUpdateService = ServiceLocator.Get<GameUpdateService>();

            _levelService.OnLevelFinish += OnLevelFinished;
            _visitableTriggerObserver.OnEnter += OnVisitableEntered;

            _gameUpdateService.Subscribe(this);

            SelfDestroyAfterDelay();
        }

        private void OnDestroy()
        {
            _levelService.OnLevelFinish -= OnLevelFinished;
            _visitableTriggerObserver.OnEnter -= OnVisitableEntered;

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

        public void Visit(EnemyBehaviour enemy)
        {
            enemy.TakeDamage(_damage);
        }

        private void OnLevelFinished()
        {
            DestroyImmediately();
        }

        private void SelfDestroyAfterDelay()
        {
            Destroy(gameObject, _aliveTime);
        }

        private void DestroyImmediately()
        {
            Destroy(gameObject);
        }

        private void OnVisitableEntered(IProjectileVisitable visitable)
        {
            visitable?.Accept(this);
        }
    }
}