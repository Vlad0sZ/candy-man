namespace Runtime.Infrastructure.UI.DragAndDrop.Interfaces
{
    public interface IDraggableTarget
    {
        void OnDragEnd();
    }
    
    public interface IDraggableTarget<in T> : IDraggableTarget
    {
        void OnDragEnd(T data);
    }
}