using Factura.Gameplay.Health;

namespace Factura.Gameplay.Attack
{
    public interface IAttack
    {
        public void Process(IHealth health);
    }
}