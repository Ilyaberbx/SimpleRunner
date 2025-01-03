using System;
using UnityEngine;
using UnityEngine.UI;

namespace Factura.UI.Popups.LevelLose
{
    public sealed class LevelLosePopupView : BasePopupView
    {
        public event Action OnContinueClick;

        [SerializeField] private Button _continueButton;

        private void Awake()
        {
            _continueButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
        }

        private void OnStartClicked()
        {
            OnContinueClick?.Invoke();
        }
    }
}