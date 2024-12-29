using System;
using UnityEngine;

namespace Gameplay.Vehicle.Modules
{
    [Serializable]
    public sealed class ModuleCreationData
    {
        [SerializeField] private BaseModuleBehaviour _prefab;

        public Type KeyType => Prefab != null ? Prefab.GetType() : null;

        public BaseModuleBehaviour Prefab => _prefab;
    }
}