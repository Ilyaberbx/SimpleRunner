using DG.Tweening;
using UnityEngine;

namespace Factura.Gameplay.Movement
{
    public interface IMovable
    {
        public Tween MoveTween(Vector3[] waypoints);
    }
}