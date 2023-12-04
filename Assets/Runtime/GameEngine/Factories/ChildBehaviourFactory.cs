using System.Collections.Generic;
using Runtime.GameEngine.Behaviours.Child.CandyBehaviours;
using Runtime.Infrastructure.RandomCore.Interfaces;

namespace Runtime.GameEngine.Factories
{
    public class ChildBehaviourFactory
    {
        private  readonly IRandom _random;

        public ChildBehaviourFactory(IRandom random) => 
            _random = random;


        public ChildCandyBehaviour GetNext()
        {
            var next = _random.Next(0, 3);

            return next switch
            {
                0 => new CandySequence(),
                1 => new CandyNot(),
                _ => new CandyOnly()
            };
        }
    }
}