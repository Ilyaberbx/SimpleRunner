using System;
using Factura.Gameplay.Modules;
using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    [Serializable]
    public sealed class ModuleCreationData
    {
        [SerializeField] private VehicleModuleBehaviour _prefab;

        public Type KeyType => Prefab != null ? Prefab.GetType() : null;

        public VehicleModuleBehaviour Prefab => _prefab;
    }
}