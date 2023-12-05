using UnityEngine;

namespace Runtime.GameEngine.Data
{
    /// <summary>
    /// Информация об уровне
    /// </summary>
    
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "GameEngine/Data/LevelInfo")]
    public class LevelInfo : ScriptableObject
    {
        public int id;
        
        /// <summary>
        /// Количество чаш с конфетами на уровне
        /// </summary>
        [Range(1, 5)] public int bowlsCount = 1;

        /// <summary>
        /// Время уровня в секундах
        /// </summary>
        [Min(1)] public int levelTimeInSeconds = 30;

        /// <summary>
        /// Количество активных детей, просящих конфеты
        /// </summary>
        [Range(1, 3)] public int activeChildrenCount = 1;

        /// <summary>
        /// Максимальное значение стреса детей, после которого уровень провалится
        /// </summary>
        [Min(100)] public int childrenStress = 100;

        public bool hasCoalBagInLevel = true;

        [SerializeField] public ChildGeneration childGeneration;
    }
}