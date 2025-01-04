using Factura.Gameplay.Enemies;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    [CreateAssetMenu(menuName = "Configs/Services/Enemy", fileName = "EnemyServiceSettings", order = 0)]
    public class EnemyServiceSettings : ScriptableObject
    {
        [SerializeField] private EnemyBehaviour _prefab;

        public EnemyBehaviour Prefab => _prefab;
    }
}