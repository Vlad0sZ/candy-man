using Runtime.GameEngine.Behaviours.Child;
using Runtime.GameEngine.Behaviours.Child.CandyBehaviours;
using UnityEngine;

namespace Runtime.GameEngine.Data
{
    public struct ChildInformation
    {
        public Camera WorldCamera;

        public ChildCandyBehaviour CandyBehaviour;

        public int MaxCandyInABag;
    }
}