using UnityEngine;

namespace Factura.UI.Services
{
    [CreateAssetMenu(menuName = "Configs/UI/Popups", fileName = "PopupsConfiguration", order = 0)]
    public sealed class PopupsConfiguration : ScriptableObject
    {
        [SerializeField] private PopupData[] _data;

        public PopupData[] Data => _data;
    }
}