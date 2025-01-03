using Better.Locators.Runtime;
using Factura.Gameplay.Services.Camera;
using Factura.Gameplay.Services.Level;
using Factura.UI.Services;

namespace Factura.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupController : BasePopupController<LevelStartModel, LevelStartPopupView>
    {
        private LevelService _levelService;
        private PopupService _popupService;
        private CameraService _cameraService;

        protected override void Show(LevelStartModel model, LevelStartPopupView view)
        {
            base.Show(model, view);

            _levelService = ServiceLocator.Get<LevelService>();
            _popupService = ServiceLocator.Get<PopupService>();
            _cameraService = ServiceLocator.Get<CameraService>();

            View.OnStartClick += OnStartClicked;
        }

        protected override void Hide()
        {
            base.Hide();

            View.OnStartClick -= OnStartClicked;
        }

        private void OnStartClicked()
        {
            _popupService.Hide();
            _cameraService.SetActive(CameraType.FollowCamera);
            _levelService.FireLevelStart();
        }
    }
}