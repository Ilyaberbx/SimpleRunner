using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;

namespace Factura.Gameplay.ModulesLocator
{
    [Serializable]
    public sealed class LocatorAttachmentData
    {
        [SerializeReference, Select(typeof(VehicleModuleBehaviour))]
        private SerializedType _serializedType;

        [SerializeField] private Transform _point;

        public Type ModuleType => _serializedType?.Type;
        public Transform Point => _point;
    }
}