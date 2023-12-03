namespace Runtime.Infrastructure.DragAndDrop.Interfaces
{
    public interface IDraggableTarget<in T>
    {
        void OnDragEnd(T data);
    }
}