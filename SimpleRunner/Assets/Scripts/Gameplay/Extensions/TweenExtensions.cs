using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

namespace Factura.Gameplay.Extensions
{
    public static class TweenExtensions
    {
        public static Task AsTask(this Tween tween, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            tween.OnComplete(() => { taskCompletionSource.TrySetResult(true); });
            tween.OnKill(() => { taskCompletionSource.TrySetCanceled(); });

            if (cancellationToken != CancellationToken.None)
            {
                tween.OnUpdate(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        tween.Kill();
                    }
                });
            }

            return taskCompletionSource.Task;
        }
    }
}