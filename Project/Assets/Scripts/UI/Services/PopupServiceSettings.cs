using System.Collections.Generic;
using UnityEngine;

namespace Factura.UI.Services
{
    [CreateAssetMenu(menuName = "Configs/Services/Popups", fileName = "PopupServiceSettings", order = 0)]
    public sealed class PopupServiceSettings : ScriptableObject
    {
        [SerializeField] private List<PopupData> data;

        public List<PopupData> Data => data;
    }
}