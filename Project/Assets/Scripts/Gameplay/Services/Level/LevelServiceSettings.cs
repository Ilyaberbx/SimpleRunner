using UnityEngine;

namespace Factura.Gameplay.Services.Level
{
    [CreateAssetMenu(menuName = "Configs/Level", fileName = "LevelConfiguration", order = 0)]
    public class LevelServiceSettings : ScriptableObject
    {
        [SerializeField] private int _levelLength;

        public int LevelLength => _levelLength;
    }
}