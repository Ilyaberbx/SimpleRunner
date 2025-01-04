using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Camera;
using Factura.Gameplay.Car.States;
using Factura.Gameplay.Health;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Movement;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.Target;
using Factura.Gameplay.Visitors;
using UnityEngine;

namespace Factura.Gameplay.Car
{
    public sealed class CarBehaviour : BaseModuleBehaviour,
        ITarget,
        ICameraTargetReadonly,
        IEnemyVisitable
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAtPoint;

        private LevelService _levelService;

        private IVehicleModulesLocator _locator;
        private IStateMachine<BaseCarState> _stateMachine;
        private IHealth _health;
        private IMovable _movement;
        private IAttachable _attachment;
        private ITarget _target;
        private ICameraTarget _cameraTarget;
        private CarMoveState _moveState;

        public Transform CameraFollow => _cameraTarget.CameraFollow;
        public Transform CameraLookAt => _cameraTarget.CameraLookAt;
        public Vector3 Position => _target.Position;

        public void Initialize(IVehicleModulesLocator locator,
            IHealth health,
            IStateMachine<BaseCarState> stateMachine,
            IMovable movement,
            IAttachable attachment,
            ITarget target,
            ICameraTarget cameraTarget)
        {
            _levelService = ServiceLocator.Get<LevelService>();

            _stateMachine = stateMachine;
            _locator = locator;
            _health = health;
            _movement = movement;
            _attachment = attachment;
            _target = target;
            _cameraTarget = cameraTarget;

            _cameraTarget.SetFollow(_cameraFollowPoint);
            _cameraTarget.SetLookAt(_cameraLookAtPoint);

            _health.OnDie += OnDied;
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;

            _stateMachine.Run();
        }

        private void OnDestroy()
        {
            _health.OnDie -= OnDied;
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        public void Attach(BaseModuleBehaviour moduleBehaviour)
        {
            _locator.Attach(moduleBehaviour);
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        public void Accept(IEnemyVisitor visitor)
        {
            visitor?.Visit(this);
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachment.TryAttach(attachmentPoint);
        }

        private void OnDied()
        {
            var deadState = new CarDeadState(this);
            _stateMachine.ChangeStateAsync(deadState, destroyCancellationToken).Forget();
            _levelService.FireLevelLose();
        }

        private void OnDestinationReached()
        {
            _moveState.OnDestinationReached -= OnDestinationReached;
            _moveState = null;
            _levelService.FireLevelWin();
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

        private void OnLevelStarted()
        {
            _moveState = new CarMoveState(_movement);
            _moveState.OnDestinationReached += OnDestinationReached;
            _stateMachine.ChangeStateAsync(_moveState, destroyCancellationToken).Forget();
        }
    }
}