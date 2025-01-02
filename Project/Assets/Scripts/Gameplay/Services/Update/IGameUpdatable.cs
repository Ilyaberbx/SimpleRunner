namespace Factura.Gameplay.Services.Update
{
    public interface IGameUpdatable
    {
        void HandleUpdate(float deltaTime);
    }
}