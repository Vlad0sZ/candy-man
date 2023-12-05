using Runtime.GameEngine.Behaviours.Child.CandyBehaviours;

namespace Runtime.GameEngine.Data
{
    public struct ChildInformation
    {
        public ChildCandyBehaviour CandyBehaviour;

        public int MaxCandyInABag;

        public  int StressIncrement;
        
        public  int StressDecrement;
        
        public  int DisappointmentIncrement;

        public float Timer;
    }
}