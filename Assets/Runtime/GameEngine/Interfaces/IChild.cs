using Runtime.GameEngine.Models;

namespace Runtime.GameEngine.Interfaces
{
    public interface IChild
    {
        ChildStatus ChildStatus { get; }

        int StressIncrement { get; }

        int StressDecrement { get; }

        void SetHierarchyIndex(int index);
        
        void DestroyChild();
    }
}