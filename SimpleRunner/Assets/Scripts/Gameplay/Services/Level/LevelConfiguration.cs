using UnityEngine;

namespace Gameplay.Services.Level
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "LevelConfiguration", order = 0)]
    public class LevelConfiguration : ScriptableObject
    {
        [SerializeField] private int _levelLength;

        public int LevelLength => _levelLength;
    }
}