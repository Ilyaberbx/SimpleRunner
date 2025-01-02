using Better.Conditions.Runtime;
using Factura.Gameplay.Attachment;
using Factura.Gameplay.Conditions;
using Factura.Gameplay.Modules.Locator;
using UnityEngine;

namespace Factura.Gameplay.Modules
{
    public sealed class TurretBehaviour : BaseModuleBehaviour
    {
        private IAttachable _attachable;


        public override void Setup(IModulesLocatorReadonly locator)
        {
            base.Setup(locator);

            _attachable = new AttachmentHandler(transform, CanAttach());
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