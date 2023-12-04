using System.Linq;
using Runtime.GameEngine.Behaviours.Bubbles;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Interfaces;

namespace Runtime.GameEngine.Behaviours.Child.CandyBehaviours
{
    public abstract class CandyWithLogic : ChildCandyBehaviour
    {
        private CandyType _rightType;
        private int _totalCandies;

        public override void Init(IRandom random, int maxCandiesCountInABag, IBubbleBuilder bubbleBuilder)
        {
            _totalCandies = maxCandiesCountInABag;
            CandyType[] allCandies = LazyCandies.Value.ToArray();
            int notCandy = random.Next(0, allCandies.Length);
            _rightType = allCandies[notCandy];

            bubbleBuilder.AddCandiesCount(_totalCandies);
            bubbleBuilder.AddCandySprite(_rightType, !IsTastelessCandy(_rightType, _rightType));
        }


        public override GiftStatus GetCandyStatus(CandyType candyType)
        {
            if (IsTastelessCandy(candyType, _rightType) == false)
                return GiftStatus.TastelessCandy;

            _totalCandies -= 1;
            return _totalCandies == 0 ? GiftStatus.EnoughCandy : GiftStatus.NeedMoreCandies;
        }
        
        protected abstract bool IsTastelessCandy(CandyType candyType, CandyType randomOneCandy);
    }
}