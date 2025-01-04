using Factura.Gameplay.Modules;
using Factura.Gameplay.Vehicle;

namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitor
    {
        void Visit(TurretBehaviour turretBehaviour);
        void Visit(VehicleBehaviour vehicleBehaviour);
    }
}