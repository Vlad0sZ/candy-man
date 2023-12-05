using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Data
{
    [CreateAssetMenu(fileName = "Child Generation for level", menuName = "GameEngine/Data/ChildLevelInfo")]
    public class ChildGeneration : ScriptableObject
    {
        [SerializeField] private Vector2Int minMaxStressIncrement;

        [SerializeField] private Vector2Int minMaxStressDecrement;

        [SerializeField] private Vector2Int minMaxDisappointmentIncrement;

        [SerializeField] private Vector2Int minMaxCandyInABag;

        [SerializeField] private Vector2Int minMaxTimer;

        public int GetStressIncrement(IRandom random) => 
            GetMinMax(random, minMaxStressIncrement);

        public int GetStressDecrement(IRandom random) => 
            GetMinMax(random, minMaxStressDecrement);

        public int GetMinMaxCandyInABag(IRandom random) => 
            GetMinMax(random, minMaxCandyInABag);

        public int GetDisappointmentIncrement(IRandom random) => 
            GetMinMax(random, minMaxDisappointmentIncrement);
        
        public float GetChildTimer(IRandom random) => 
            GetMinMax(random, minMaxTimer);

        private static int GetMinMax(IRandom random, Vector2Int minMax) =>
            random.Next(minMax.x, minMax.y);
    }
}