using UnityEngine;

namespace Factura.Gameplay.Services.Level
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Level", fileName = "LevelConfiguration", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] private int _length;

        public int Length => _length;
    }
}