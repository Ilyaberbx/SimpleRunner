using Better.Commons.Runtime.Extensions;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using Better.StateMachine.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Launcher;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Modules.States;
using Factura.Gameplay.Services.Level;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class TurretBehaviour : BaseModuleBehaviour
    {
        [SerializeField] private TurretLauncherConfiguration _launcherConfiguration;

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
            _launcher = new TurretLauncher(cachedTransform, Camera.main, _launcherConfiguration);
            _attachable = new AttachmentHandler(cachedTransform, CanAttach());
            _levelService = ServiceLocator.Get<LevelService>();
            _levelService.OnLevelStart += OnLevelStarted;
        }

        private void OnDestroy()
        {
            _levelService.OnLevelStart -= OnLevelStarted;
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