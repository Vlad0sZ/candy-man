using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Child
{
    public abstract class ChildCandyBehaviour : MonoBehaviour
    {

        public abstract void Init(IRandom random, int maxCandiesCountInABag);
        
        
        public abstract GiftStatus GetCandyStatus(CandyType candyType);
    }
}