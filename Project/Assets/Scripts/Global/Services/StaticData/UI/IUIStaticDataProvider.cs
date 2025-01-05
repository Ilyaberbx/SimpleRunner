using Factura.UI.Services;

namespace Factura.Global.Services.StaticData
{
    public interface IUIStaticDataProvider
    {
        PopupData GetPopupConfiguration(PopupType type);
    }
}