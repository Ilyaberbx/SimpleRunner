using Better.Conditions.Runtime;
using Gameplay.Vehicle.Modules.Attachment;
using Gameplay.Vehicle.Modules.Conditions;
using Gameplay.Vehicle.Modules.Locator;
using UnityEngine;

namespace Gameplay.Vehicle.Modules
{
    public sealed class TurretBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachmentHandler;

        protected override bool TryAttachInternal(IModulesLocatorReadonly locator, Transform attachmentPoint)
        {
            _attachmentHandler = new AttachmentHandler(transform, CanAttach());
            return _attachmentHandler.Attach(locator, attachmentPoint);
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

        public override void Detach()
        {
            _attachmentHandler?.Detach();
        }
    }
}