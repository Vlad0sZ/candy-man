using System;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using Runtime.Infrastructure.Models;
using UnityEngine;

namespace Runtime.Infrastructure.DragAndDrop.Behaviours
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DragAndDrop2D<T> : MonoBehaviour
    {
        private const int MaxHitCount = 25;
        [SerializeField] private Camera gameCamera;
        [SerializeField] private Transform draggableParent;
        
        protected ListenableValue<bool> IsDragging { get; } = new();

        protected Camera GameCamera
        {
            get => gameCamera;
            
            set => gameCamera = value;
        }

        protected Transform DraggableParent
        {
            get
            {
                if (_selfTransform == null)
                    _selfTransform = draggableParent == null ? transform : draggableParent;

                return _selfTransform;
            }
        }

        private RaycastHit2D[] RaycastBuffer
        {
            get
            {
                // max 25 objects;
                if (_raycastBuffer == null)
                    _raycastBuffer = new RaycastHit2D[MaxHitCount];

                return _raycastBuffer;
            }
        }

        private RaycastHit2D[] _raycastBuffer;
        private Transform _selfTransform;
        private Transform _dragChild;

        private void OnMouseDown()
        {
            DestroyChild();

            _dragChild = CreateDraggableChild(DraggableParent);
            IsDragging.Value = true;
        }

        private void OnMouseUp()
        {
            DestroyChild();
            
            // cast to IDraggableTarget
            var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            var size = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, RaycastBuffer);

            for (int i = 0; i < size; i++)
            {
                var hit = RaycastBuffer[i];
                var component = hit.transform.GetComponent<IDraggableTarget<T>>();
                if (component != null)
                    OnDragTarget(component);
            }

            IsDragging.Value = false;
        }

        private void OnMouseDrag()
        {
            if (IsDragging.Value == false)
                throw new Exception("Is Dragging was false");

            if (_dragChild == null)
                throw new Exception("Draggable child was null");

            var mouseInWorld = gameCamera.ScreenToWorldPoint(MouseToVector3());
            var delta = mouseInWorld - _dragChild.position;
            _dragChild.Translate(delta);
        }

        private void DestroyChild()
        {
            if(_dragChild != null)
                Destroy(_dragChild.gameObject);
        }

        private Vector3 MouseToVector3()
        {
            var mousePosition = Input.mousePosition;
            return new Vector3(mousePosition.x, mousePosition.y, gameCamera.nearClipPlane);
        }

        protected abstract Transform CreateDraggableChild(Transform parent);
        
        protected abstract void OnDragTarget(IDraggableTarget<T> target);
    }
}