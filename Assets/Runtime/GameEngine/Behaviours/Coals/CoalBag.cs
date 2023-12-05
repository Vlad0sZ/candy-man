using Runtime.Infrastructure.DragAndDrop.Behaviours;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Coals
{
    public class CoalBag : DragAndDrop2D<Models.Coal>
    {
        [SerializeField] private GameObject coalPrefab;

        public void Init(Camera worldCamera) => 
            GameCamera = worldCamera;

        protected override Transform CreateDraggableChild(Transform parent) => 
            Instantiate(coalPrefab, parent, true).transform;

        protected override void OnDragTarget(IDraggableTarget<Models.Coal> target) => 
            target.OnDragEnd(default);
    }
}