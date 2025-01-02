using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Factura.UI.MVC;
using Factura.UI.Popups;
using UnityEngine;

namespace Factura.UI.Services
{
    [Serializable]
    public sealed class PopupData
    {
        [SerializeReference, Select(typeof(BaseController))]
        private SerializedType _type;

        [SerializeField] private BasePopupView viewPrefab;

        public BaseView ViewPrefab => viewPrefab;
        public SerializedType Type => _type;
    }
}