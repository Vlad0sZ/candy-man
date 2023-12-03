using System;
using System.Collections.Generic;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Child.CandyBehaviours
{
    public class CandySequence : ChildCandyBehaviour
    {
        [SerializeField]
        private int maxCandies;

        private IRandom _random;
        private Stack<CandyType> _candies;

        public override void Init(IRandom random, int maxCandiesCountInABag)
        {
            maxCandies = maxCandiesCountInABag;
            _candies = new Stack<CandyType>(maxCandies);
            _random = random;

            CreateSequence();

            Debug.Log($"Candies: {String.Join(',', _candies)}");
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



        private void CreateSequence()
        {
            var totalCandies = Enum.GetValues(typeof(CandyType));
            var maxIndex = totalCandies.Length;

            for (int i = 0; i < maxCandies; i++)
            {
                var nextCandyIndex = _random.Next(maxIndex);
                var nextCandy = (CandyType) totalCandies.GetValue(nextCandyIndex);
                _candies.Push(nextCandy);
            }

        }
    }
    
}