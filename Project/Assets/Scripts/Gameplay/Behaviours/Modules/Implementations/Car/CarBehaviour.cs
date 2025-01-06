using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Camera;
using Factura.Gameplay.Car.States;
using Factura.Gameplay.Enemy.Stickman;
using Factura.Gameplay.Health;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Camera;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Car
{
    public sealed class CarBehaviour : VehicleModuleBehaviour, IEnemyVisitor, ITarget
    {
        [SerializeField] private Transform _turretAttachmentPoint;
        [SerializeField] private Transform _bulletsPackAttachmentPoint;
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAtPoint;

        private LevelService _levelService;

        private IStateMachine<BaseCarState> _stateMachine;
        private IMovable _movement;
        private IAttachable _attachment;
        private IHealth _health;
        private CarMoveState _moveState;

        public Vector3 Position => Target.Position;
        public ITarget Target { get; private set; }
        public ICameraTarget CameraTarget { get; private set; }
        public IVehicleModulesLocator ModulesLocator { get; private set; }

        public void Initialize(IVehicleModulesLocator locator,
            IHealth health,
            IStateMachine<BaseCarState> stateMachine,
            IMovable movement,
            IAttachable attachment,
            ITarget target)
        {
            _levelService = ServiceLocator.Get<LevelService>();

            _stateMachine = stateMachine;
            ModulesLocator = locator;
            _health = health;
            _movement = movement;
            _attachment = attachment;
            Target = target;
            CameraTarget = new CameraTargetComponent(_cameraFollowPoint, _cameraLookAtPoint);

            ModulesLocator.RegisterAttachment(VehicleModuleType.BulletsPack, _bulletsPackAttachmentPoint);
            ModulesLocator.RegisterAttachment(VehicleModuleType.Turret, _turretAttachmentPoint);

            _health.OnDie += OnDied;
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;

            _stateMachine.Run();
        }

        private void OnDestroy()
        {
            ModulesLocator.UnregisterAttachment(VehicleModuleType.BulletsPack);
            ModulesLocator.UnregisterAttachment(VehicleModuleType.Turret);

            _health.OnDie -= OnDied;
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachment.TryAttach(attachmentPoint);
        }

        private void OnLevelStarted()
        {
            _moveState = new CarMoveState(_movement);
            _moveState.OnDestinationReached += OnDestinationReached;
            _stateMachine.ChangeStateAsync(_moveState, destroyCancellationToken).Forget();
        }

        private void OnLevelFinished()
        {
            if (!_stateMachine.IsRunning)
            {
                return;
            }

            _stateMachine.ChangeStateAsync(new CarIdleState(), destroyCancellationToken).Forget();
            _stateMachine.Stop();
        }

        private void OnDied()
        {
            var deadState = new CarDeadState(ModulesLocator.AttachedModules);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken).Forget();
            _levelService.FireLevelLose();
        }

        private void OnDestinationReached()
        {
            _moveState.OnDestinationReached -= OnDestinationReached;
            _moveState = null;
            _levelService.FireLevelWin();
        }

        public void Visit(StickmanBehaviour stickmanBehaviour)
        {
            stickmanBehaviour.Attack.Process(_health);
        }
    }
}