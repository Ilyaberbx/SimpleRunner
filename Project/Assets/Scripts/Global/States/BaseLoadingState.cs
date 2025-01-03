using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using UnityEngine.SceneManagement;

namespace Factura.Global.States
{
    public abstract class BaseLoadingState : BaseGameState
    {
        protected abstract string GetSceneName();

        public sealed override async Task EnterAsync(CancellationToken token)
        {
            await base.EnterAsync(token);

            var sceneName = GetSceneName();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            var loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
        }

        public override async Task ExitAsync(CancellationToken token)
        {
            var sceneName = GetSceneName();
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}