using Better.Locators.Runtime;
using Factura.Gameplay.Enemies;
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

        private Vector3 _direction;
        private LevelService _levelService;
        private GameUpdateService _gameUpdateService;
        private Transform _cachedTransform;

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
            var movement = _direction * _moveSpeed * deltaTime;
            _cachedTransform.position += movement;
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