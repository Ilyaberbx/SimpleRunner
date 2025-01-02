using System;
using UnityEngine;

namespace Factura.Gameplay.Launcher
{
    [Serializable]
    public sealed class TurretLauncherConfiguration
    {
        [SerializeField] private float _fireCooldown;
        [SerializeField] private GameObject _projectilePrefab;

        public float FireCooldown => _fireCooldown;

        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}