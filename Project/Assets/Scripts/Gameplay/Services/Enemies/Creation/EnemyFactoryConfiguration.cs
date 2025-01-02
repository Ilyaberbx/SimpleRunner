using System;
using Factura.Gameplay.Enemies;
using UnityEngine;

namespace Factura.Gameplay.Services.Enemies
{
    [Serializable]
    public class EnemyFactoryConfiguration
    {
        [SerializeField] private EnemyBehaviour _prefab;

        public EnemyBehaviour Prefab => _prefab;
    }
}