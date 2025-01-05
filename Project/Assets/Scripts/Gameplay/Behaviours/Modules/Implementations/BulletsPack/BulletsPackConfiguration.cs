using UnityEngine;

namespace Factura.Gameplay.BulletsPack
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Car", fileName = "CarConfiguration", order = 0)]
    public sealed class BulletsPackConfiguration : BaseModuleConfiguration
    {
        [SerializeField] private BulletsPackBehaviour _prefab;

        public BulletsPackBehaviour Prefab => _prefab;
    }
}