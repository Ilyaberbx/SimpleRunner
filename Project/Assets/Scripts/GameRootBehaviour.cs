using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Factura.Global.Services.StatesManagement;
using Factura.Global.States;
using UnityEngine;

namespace Factura
{
    public sealed class GameRootBehaviour : MonoBehaviour
    {
        private GameStatesService _gameStatesService;

        private void Start()
        {
            _gameStatesService = ServiceLocator.Get<GameStatesService>();
            _gameStatesService.ChangeStateAsync<GameplayState>().Forget();
        }

        private void OnDestroy()
        {
            _gameStatesService.Dispose();
        }
    }
}