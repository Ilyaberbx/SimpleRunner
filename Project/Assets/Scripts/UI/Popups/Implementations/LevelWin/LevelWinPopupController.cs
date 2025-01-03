using Better.Locators.Runtime;
using Factura.Global.Services;
using Factura.Global.States;
using Factura.UI.Services;

namespace Factura.UI.Popups.LevelWin
{
    public sealed class LevelWinPopupController : BasePopupController<LevelWinPopupModel, LevelWinPopupView>
    {
        private GameStatesService _gameStatesService;
        private PopupService _popupService;

        protected override void Show(LevelWinPopupModel model, LevelWinPopupView view)
        {
            base.Show(model, view);

            _gameStatesService = ServiceLocator.Get<GameStatesService>();
            _popupService = ServiceLocator.Get<PopupService>();

            View.OnContinueClick += OnContinueClicked;
        }

        protected override void Hide()
        {
            base.Hide();

            View.OnContinueClick -= OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            _popupService.Hide();
            _gameStatesService.ChangeStateAsync<GameplayState>();
        }
    }
}