namespace Factura.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName()
        {
            return SceneConstants.Gameplay;
        }
    }
}