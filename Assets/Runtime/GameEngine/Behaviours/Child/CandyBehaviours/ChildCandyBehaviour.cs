using System;
using System.Collections.Generic;
using Runtime.GameEngine.Behaviours.Bubbles;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Interfaces;
using Runtime.Infrastructure.Utility;

namespace Runtime.GameEngine.Behaviours.Child.CandyBehaviours
{
    public abstract class ChildCandyBehaviour
    {

        protected static Lazy<IEnumerable<CandyType>> LazyCandies => new(EnumExtensions.GetAllValues<CandyType>);


        public abstract void Init(IRandom random, int maxCandiesCountInABag, IBubbleBuilder bubbleBuilder);
        
        
        public abstract GiftStatus GetCandyStatus(CandyType candyType);
    }
}