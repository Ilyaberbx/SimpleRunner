using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;
using UnityEngine;

namespace Factura.Gameplay.Services.Update
{
    public sealed class GameUpdateService : MonoService
    {
        private const string CanNotUnsubscribeFormat = "Can not unsubscribe element {0}";
        private const string ElementAlreadyAddedFormat = "Element {0} already added";

        private readonly List<IGameUpdatable> _elements = new();

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void Update()
        {
            if (_elements.IsNullOrEmpty())
            {
                return;
            }

            foreach (var element in _elements)
            {
                element.HandleUpdate(Time.deltaTime);
            }
        }

        public void Subscribe(IGameUpdatable element)
        {
            if (_elements.Contains(element))
            {
                var message = string.Format(ElementAlreadyAddedFormat, element.GetType().Name);
                Debug.LogWarning(message);
                return;
            }

            _elements.Add(element);
        }

        public void Unsubscribe(IGameUpdatable element)
        {
            var hasElement = _elements.Remove(element);

            if (hasElement) return;

            var message = string.Format(CanNotUnsubscribeFormat, element.GetType().Name);
            Debug.LogWarning(message);
        }
    }
}