using System;
using Better.Conditions.Runtime;
using Better.Locators.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Services.Level;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class TurretBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachable;
        private LevelService _levelService;

        public override void Setup(IModulesLocatorReadonly locator)
        {
            base.Setup(locator);

            _attachable = new AttachmentHandler(transform, CanAttach());
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