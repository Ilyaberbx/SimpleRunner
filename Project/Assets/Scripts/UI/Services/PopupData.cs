using System;
using Factura.UI.MVC;
using Factura.UI.Popups;
using UnityEngine;

namespace Factura.UI.Services
{
    [Serializable]
    public sealed class PopupData
    {
        [SerializeField] private PopupType _type;
        [SerializeField] private BasePopupView viewPrefab;

        public BaseView ViewPrefab => viewPrefab;
        public PopupType Type => _type;
    }
}