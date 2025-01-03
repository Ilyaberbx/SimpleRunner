namespace Factura.Gameplay.Visitors
{
    public interface IProjectileVisitable
    {
        void Accept(IProjectileVisitor visitor);
    }
}