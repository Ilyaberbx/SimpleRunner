using Better.Locators.Runtime;
using Factura.Global.Services;
using Factura.Global.States;
using Factura.UI.Popups.LevelWin;
using Factura.UI.Services;

namespace Factura.UI.Popups.LevelLose
{
    public sealed class LevelLosePopupController : BasePopupController<LevelLosePopupModel, LevelLosePopupView>
    {
        private GameStatesService _gameStatesService;
        private PopupService _popupService;

        protected override void Show(LevelLosePopupModel model, LevelLosePopupView view)
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