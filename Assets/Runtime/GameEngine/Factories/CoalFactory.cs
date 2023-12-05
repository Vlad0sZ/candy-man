using Runtime.GameEngine.Behaviours.Candies;
using Runtime.GameEngine.Behaviours.Coals;
using Runtime.Infrastructure.FactoryBase;
using UnityEngine;

namespace Runtime.GameEngine.Factories
{
    public class CoalFactory : AbstractSequenceFactory<CoalBag>
    {
        [SerializeField] private CoalBag prefab;
        [SerializeField] private Camera gameCamera;

        public override CoalBag InstantiateNext(Transform parent, bool worldPositionsStay)
        {
            var coalBag = Instantiate(prefab, parent, true);
            coalBag.Init(gameCamera);

            return coalBag;
        }
    }
}