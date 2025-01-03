using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Modules.States;
using Factura.Gameplay.Projectiles;
using Factura.Gameplay.Rotator;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Shooter;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class TurretBehaviour : BaseModuleBehaviour
    {
        [SerializeField] private BaseProjectileBehaviour _projectilePrefab;
        [SerializeField] private float _fireCooldown;
        [SerializeField] private Transform _shootPoint;

        private LevelService _levelService;

        private IAttachable _attachable;
        private ILauncher _launcher;
        private IStateMachine<BaseTurretState> _stateMachine;

        public override void Setup(IModulesLocatorReadonly locator)
        {
            base.Setup(locator);

            var cachedTransform = transform;
            _stateMachine = new StateMachine<BaseTurretState>();
            _stateMachine.Run();
            var shooter = new ProjectileShooter(_shootPoint, _projectilePrefab);
            var rotator = new TurretRotator(cachedTransform);
            _launcher = new TurretLauncher(Camera.main, _fireCooldown, rotator, shooter);
            _attachable = new AttachmentHandler(cachedTransform, CanAttach());
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        private void OnLevelStarted()
        {
            if (!IsAttached)
            {
                return;
            }

            var activeState = new TurretActiveState(_launcher);
            _stateMachine.ChangeStateAsync(activeState, destroyCancellationToken).Forget();
        }

        private void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning)
            {
                return;
            }

            _stateMachine.ChangeStateAsync(new TurretIdleState(), destroyCancellationToken).Forget();
            _stateMachine.Stop();
        }

        private AllComplexCondition CanAttach()
        {
            var condition = new AllComplexCondition(new Condition[]
            {
                new ActiveSelfCondition(gameObject, true),
                new HasModuleCondition(typeof(BulletsPackBehaviour), Locator)
            });

            condition.Rebuild();
            return condition;
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachable.TryAttach(attachmentPoint);
        }
    }
}