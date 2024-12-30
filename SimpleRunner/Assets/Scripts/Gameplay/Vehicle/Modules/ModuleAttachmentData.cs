using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;

namespace Gameplay.Vehicle.Modules
{
    [Serializable]
    public sealed class ModuleAttachmentData
    {
        [SerializeReference, Select(typeof(BaseModuleBehaviour))]
        private SerializedType _serializedType;

        [SerializeField] private Transform _point;

        public Type ModuleType => _serializedType?.Type;
        public Transform Point => _point;
    }
}