using Runtime.GameEngine.Behaviours.Candies;
using Runtime.Infrastructure.FactoryBase;
using UnityEngine;

namespace Runtime.GameEngine.Factories
{
    public class BowlFactory : AbstractSequenceFactory<CandyBowl>
    {
        [SerializeField] private CandyBowl bowlPrefab;
        [SerializeField] private CandyFactory candyFactory;
        [SerializeField] private Camera gameCamera;
        
        public override CandyBowl InstantiateNext(Transform parent, bool worldPositionsStay)
        {
            var bowl = Instantiate(bowlPrefab, parent, worldPositionsStay);
            bowl.Init(candyFactory, gameCamera);
            return bowl;
        }
    }
}