namespace Factura.Gameplay.Visitors
{
    public interface IEnemyVisitable
    {
        void Accept(IEnemyVisitor visitor);
    }
}