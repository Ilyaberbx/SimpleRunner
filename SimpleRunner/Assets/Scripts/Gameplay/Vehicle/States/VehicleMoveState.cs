using Factura.Gameplay.Movement;

namespace Factura.Gameplay.Vehicle.States
{
    public sealed class VehicleMoveState : BaseVehicleState
    {
        private readonly MoveState _moveState;

        public VehicleMoveState(IMovable movable)
        {
            _moveState = new MoveState(movable);
        }

        protected override void Enter()
        {
            _moveState.Enter();
        }

        protected override void Exit()
        {
            _moveState.Exit();
        }
    }
}