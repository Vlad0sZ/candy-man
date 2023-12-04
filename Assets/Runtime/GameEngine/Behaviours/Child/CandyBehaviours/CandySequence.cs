using System.Collections.Generic;
using System.Linq;
using Runtime.GameEngine.Behaviours.Bubbles;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Interfaces;
using Runtime.Infrastructure.Utility;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Child.CandyBehaviours
{
    public class CandySequence : ChildCandyBehaviour
    {
        private const int MinSequenceCount = 2;
        
        private IRandom _random;
        private Stack<CandyType> _candies;
        private int _maxSequence;

        public override void Init(IRandom random, int maxCandiesCountInABag, IBubbleBuilder bubbleBuilder)
        {
            _maxSequence = Mathf.Min(maxCandiesCountInABag, MinSequenceCount);
            _random = random;
            CreateSequence();

            foreach (var candy in _candies) 
                bubbleBuilder.AddCandySprite(candy, false);
        }

        public override GiftStatus GetCandyStatus(CandyType candyType)
        {
            if (_candies.Count == 0)
                return GiftStatus.EnoughCandy;
            
            
            var candy = _candies.Peek();

            if (candy != candyType)
            {
                Debug.LogError($"Gift {candyType} but expected {candy}");
                return GiftStatus.TastelessCandy;
            }
            
            _candies.Pop();
            return _candies.Count == 0 ? GiftStatus.EnoughCandy : GiftStatus.NeedMoreCandies;
        }



        private void CreateSequence() => 
            _candies = LazyCandies.Value.Take(_maxSequence).ToShuffleStack(_random);
    }
    
}