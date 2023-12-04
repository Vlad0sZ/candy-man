using Runtime.GameEngine.Models;

namespace Runtime.GameEngine.Behaviours.Child.CandyBehaviours
{
    public class CandyNot : CandyWithLogic
    {
        protected override bool IsTastelessCandy(CandyType candyType, CandyType randomOneCandy) => 
            candyType == randomOneCandy;
    }
}