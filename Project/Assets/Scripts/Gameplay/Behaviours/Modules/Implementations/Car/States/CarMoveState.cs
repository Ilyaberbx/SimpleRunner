using System;
using System.Threading;
using System.Threading.Tasks;
using Factura.Gameplay.Movement;

namespace Factura.Gameplay.Car.States
{
    public sealed class CarMoveState : BaseCarState
    {
        public event Action OnDestinationReached
        {
            add => _moveState.OnDestinationReached += value;
            remove => _moveState.OnDestinationReached -= value;
        }

        private readonly MoveState _moveState;

        public CarMoveState(IMovable movable)
        {
            _moveState = new MoveState(movable);
        }

        public override Task EnterAsync(CancellationToken token)
        {
            return _moveState.EnterAsync(token);
        }

        public override Task ExitAsync(CancellationToken token)
        {
            return _moveState.ExitAsync(token);
        }
    }
}