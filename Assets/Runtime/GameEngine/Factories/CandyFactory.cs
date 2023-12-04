using Runtime.GameEngine.Behaviours.Candies;
using Runtime.GameEngine.Data;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.FactoryBase;
using UnityEngine;

namespace Runtime.GameEngine.Factories
{
    public class CandyFactory : AbstractFactoryByData<CandyType, Candy>
    {
        [SerializeField] private Candy candyPrefab;
        [SerializeField] private CandySprites candySprites;

        public  override Candy Instantiate(CandyType byType, Transform parent, bool worldPositionStays)
        {
            var sprite = candySprites.GetSprite(byType);
            var candy =  Instantiate(candyPrefab, parent, worldPositionStays);
            candy.Renderer.sprite = sprite;

            return candy;
        }
    }
}