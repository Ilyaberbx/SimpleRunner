using System.Collections.Generic;
using Factura.Gameplay.ModulesLocator;
using Factura.Gameplay.Movement.Waypoints;
using UnityEngine;

namespace Factura.Gameplay.Car
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Car", fileName = "CarConfiguration", order = 0)]
    public sealed class CarConfiguration : BaseModuleConfiguration
    {
        [SerializeField] private CarBehaviour _prefab;
        [SerializeField] private int _healthAmount;
        [SerializeField] private LocatorAttachmentData[] _attachmentConfiguration;
        [SerializeField] private MoveByWaypointsConfiguration _movementConfiguration;

        public IReadOnlyList<LocatorAttachmentData> AttachmentConfiguration => _attachmentConfiguration;
        public int HealthAmount => _healthAmount;
        public MoveByWaypointsConfiguration MovementConfiguration => _movementConfiguration;
        public CarBehaviour Prefab => _prefab;
    }
}