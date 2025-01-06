namespace Factura.Gameplay.Animations
{
    public interface IStickmanAnimator
    {
        void PlayPatrol(bool value);
        void PlayAttack();
        void PlayChase(bool value);
        void PlayIdle();
    }
}