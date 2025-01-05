using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Camera;
using Factura.Gameplay.Car.States;
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
    public sealed class CarBehaviour : VehicleModuleBehaviour, IEnemyVisitable
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAtPoint;

        private LevelService _levelService;

        private IStateMachine<BaseCarState> _stateMachine;
        private IMovable _movement;
        private IAttachable _attachment;
        private CarMoveState _moveState;
        
        public IHealth Health { get; private set; }
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
            Health = health;
            _movement = movement;
            _attachment = attachment;
            Target = target;
            CameraTarget = new CameraTargetComponent(_cameraFollowPoint, _cameraLookAtPoint);

            Health.OnDie += OnDied;
            _levelService.OnLevelStart += OnLevelStarted;
            _levelService.OnLevelFinish += OnLevelFinished;

            _stateMachine.Run();
        }

        private void OnDestroy()
        {
            Health.OnDie -= OnDied;
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
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