using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;

namespace Factura.Gameplay.Modules.Locator
{
    [Serializable]
    public sealed class LocatorAttachmentConfiguration
    {
        [SerializeReference, Select(typeof(BaseModuleBehaviour))]
        private SerializedType _serializedType;

        [SerializeField] private Transform _point;

        public Type ModuleType => _serializedType?.Type;
        public Transform Point => _point;
    }
}