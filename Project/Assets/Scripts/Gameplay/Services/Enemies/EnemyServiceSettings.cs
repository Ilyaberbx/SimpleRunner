using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    [CreateAssetMenu(menuName = "Configs/Services/Enemy", fileName = "EnemyServiceSettings", order = 0)]
    public class EnemyServiceSettings : ScriptableObject
    {
        [SerializeField] private EnemyFactoryConfiguration _factoryConfiguration;

        public EnemyFactoryConfiguration FactoryConfiguration => _factoryConfiguration;
    }
}