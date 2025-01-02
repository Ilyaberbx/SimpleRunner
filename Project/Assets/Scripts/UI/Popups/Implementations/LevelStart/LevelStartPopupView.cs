using System;
using UnityEngine;
using UnityEngine.UI;

namespace Factura.UI.Popups.LevelStart
{
    public sealed class LevelStartPopupView : BasePopupView
    {
        public event Action OnStartClick;

        [SerializeField] private Button _startButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnStartClicked()
        {
            OnStartClick?.Invoke();
        }
    }
}