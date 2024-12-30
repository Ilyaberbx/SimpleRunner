using UnityEngine;

namespace Gameplay.Services
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "LevelConfiguration", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] private int _levelLength;
        [SerializeField] private int _turnChance;
        [SerializeField] private int _turnRange;
        [SerializeField] private int _resolution;

        public int LevelLength => _levelLength;
        public int TurnChance => _turnChance;
        public int TurnRange => _turnRange;
        public int Resolution => _resolution;
    }
}