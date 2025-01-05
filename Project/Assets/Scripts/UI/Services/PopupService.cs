using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Factura.Global.Services.StaticData;
using Factura.UI.Helpers;
using Factura.UI.MVC;
using Factura.UI.Popups.LevelLose;
using Factura.UI.Popups.LevelStart;
using Factura.UI.Popups.LevelWin;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.UI.Services
{
    [Serializable]
    public sealed class PopupService : PocoService
    {
        [SerializeField] private CanvasGroup _backgroundGroup;
        [SerializeField] private Transform _root;

        private BaseController _currentController;
        private readonly IDictionary<PopupType, Type> _controllerPopupMap = new Dictionary<PopupType, Type>();
        private IUIStaticDataProvider _staticDataProvider;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            HideBackground();

            _controllerPopupMap.Add(PopupType.LevelWin, typeof(LevelWinPopupController));
            _controllerPopupMap.Add(PopupType.LevelLose, typeof(LevelLosePopupController));
            _controllerPopupMap.Add(PopupType.LevelStart, typeof(LevelStartPopupController));

            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _staticDataProvider = ServiceLocator.Get<UIStaticDataService>();
            return Task.CompletedTask;
        }

        public TController Show<TController, TModel>(PopupType type, TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var isValid = ValidateController<TController>(type);

            if (!isValid)
            {
                return null;
            }

            var data = _staticDataProvider.GetPopupConfiguration(type);

            if (data == null || data.ViewPrefab == null)
            {
                PopupsDebugHelper.PrintCannotFindView(type);
                return default;
            }

            Hide();

            var viewPrefab = data.ViewPrefab;
            var at = _root.GetComponent<RectTransform>().position;
            var view = Object.Instantiate(viewPrefab, at, Quaternion.identity, _root);
            var controller = new TController();

            controller.Initialize(view, model);
            _currentController = controller;

            ShowBackground();
            return controller;
        }

        public void Hide()
        {
            if (_currentController == null)
            {
                return;
            }

            var viewGameObject = _currentController.DerivedView.gameObject;
            _currentController.Dispose();
            _currentController = null;
            Object.Destroy(viewGameObject);
            HideBackground();
        }

        private bool ValidateController<TRequestedController>(PopupType type)
            where TRequestedController : BaseController, new()
        {
            var requestedControllerType = typeof(TRequestedController);

            if (_controllerPopupMap.TryGetValue(type, out var controllerType))
            {
                if (controllerType == requestedControllerType) return true;

                PopupsDebugHelper.PrintMappingMismatched<TRequestedController>(controllerType, type);
                return false;
            }


            PopupsDebugHelper.PrintCannotFindController(type);
            return false;
        }

        private void ShowBackground()
        {
            _backgroundGroup.alpha = 1;
            _backgroundGroup.interactable = true;
            _backgroundGroup.blocksRaycasts = true;
        }

        private void HideBackground()
        {
            _backgroundGroup.alpha = 0;
            _backgroundGroup.interactable = false;
            _backgroundGroup.blocksRaycasts = false;
        }
    }
}