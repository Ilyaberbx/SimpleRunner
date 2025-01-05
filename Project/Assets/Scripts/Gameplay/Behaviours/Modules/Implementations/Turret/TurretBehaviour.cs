using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.BulletsPack;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Services.Level;
using Factura.Gameplay.States;
using UnityEngine;

namespace Factura.Gameplay
{
    public sealed class TurretBehaviour : VehicleModuleBehaviour
    {
        [SerializeField] private Transform _shootPoint;

        private LevelService _levelService;

        private IAttachable _attachment;
        private ILauncher _launcher;
        private IStateMachine<BaseTurretState> _stateMachine;
        private Condition _attachmentCondition;

        public Transform ShootPoint => _shootPoint;

        public void Initialize(IAttachable attachment,
            ILauncher launcher,
            IStateMachine<BaseTurretState> stateMachine)
        {
            _attachment = attachment;
            _launcher = launcher;
            _stateMachine = stateMachine;
            _levelService = ServiceLocator.Get<LevelService>();
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
            _levelService.OnLevelFinish -= OnLevelFinished;
        }

        public override void SetupLocator(IVehicleModulesLocatorReadonly locator)
        {
            base.SetupLocator(locator);
            _attachmentCondition = new HasModuleCondition(typeof(BulletsPackBehaviour), locator);
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
            if (!_stateMachine.IsRunning || _stateMachine.CurrentState is not TurretActiveState)
            {
                return;
            }

            _stateMachine.ChangeStateAsync(new TurretIdleState(), destroyCancellationToken).Forget();
            _stateMachine.Stop();
        }

        protected override bool TryAttachInternal(Transform attachmentPoint)
        {
            return _attachmentCondition.SafeInvoke() && _attachment.TryAttach(attachmentPoint);
        }
    }
}