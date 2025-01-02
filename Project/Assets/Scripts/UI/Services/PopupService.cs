using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Factura.UI.MVC;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.UI.Services
{
    [Serializable]
    public sealed class PopupService : PocoService<PopupServiceSettings>
    {
        [SerializeField] private CanvasGroup _backgroundGroup;
        [SerializeField] private Transform _root;

        private BaseController _currentController;
        private const string CanNotFindViewFormat = "Can not find view for controller {0}";

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);
            HideBackground();
        }

        public TController Show<TController, TModel>(TModel model)
            where TController : BaseController<TModel>, new()
            where TModel : IModel
        {
            var controllerType = typeof(TController);
            var data = Settings.Data.FirstOrDefault(temp => temp.Type == controllerType);

            if (data == null || data.ViewPrefab == null)
            {
                var message = string.Format(CanNotFindViewFormat, typeof(TController).Name);
                Debug.LogError(message);
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
            Object.Destroy(viewGameObject);
            HideBackground();
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