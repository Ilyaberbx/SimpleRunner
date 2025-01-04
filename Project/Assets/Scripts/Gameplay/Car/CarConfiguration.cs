using System.Collections.Generic;
using Factura.Gameplay.Modules.Locator;
using Factura.Gameplay.Movement.Waypoints;
using UnityEngine;

namespace Factura.Gameplay.Car
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Car", fileName = "CarConfiguration", order = 0)]
    public sealed class CarConfiguration : ScriptableObject
    {
        [SerializeField] private int _healthAmount;
        [SerializeField] private LocatorAttachmentConfiguration[] _attachmentConfiguration;
        [SerializeField] private MoveByWaypointsConfiguration _movementConfiguration;

        public IReadOnlyList<LocatorAttachmentConfiguration> AttachmentConfiguration => _attachmentConfiguration;
        public int HealthAmount => _healthAmount;

        public MoveByWaypointsConfiguration MovementConfiguration => _movementConfiguration;
    }
}