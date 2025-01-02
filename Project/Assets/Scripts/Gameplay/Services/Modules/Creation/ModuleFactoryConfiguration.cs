using System;
using System.Collections.Generic;
using UnityEngine;

namespace Factura.Gameplay.Services.Modules
{
    [Serializable]
    public sealed class ModuleFactoryConfiguration
    {
        [SerializeField] private ModuleCreationData[] _creationData;

        public IReadOnlyCollection<ModuleCreationData> CreationData => _creationData;
    }
}