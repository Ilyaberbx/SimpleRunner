using UnityEngine;

namespace Factura.Gameplay.Vehicle.States
{
    public sealed class VehicleDeadState : BaseVehicleState
    {
        private readonly GameObject _source;

        public VehicleDeadState(GameObject source)
        {
            _source = source;
        }

        protected override void Enter()
        {
            Object.Destroy(_source);
        }

        protected override void Exit()
        {
        }
    }
}