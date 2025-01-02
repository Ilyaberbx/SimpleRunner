using Factura.UI.MVC;

namespace Factura.UI.Popups
{
    public abstract class BasePopupController<TModel, TView> : BaseController<TModel, TView>
        where TView : BasePopupView
        where TModel : IModel
    {
    }
}