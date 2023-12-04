using Runtime.GameEngine.Models;

namespace Runtime.GameEngine.Behaviours.Bubbles
{
    public interface IBubbleBuilder
    {
        public IBubbleBuilder AddCandiesCount(int count);

        public IBubbleBuilder AddCandySprite(CandyType candyType, bool notPrefab);
    }
}