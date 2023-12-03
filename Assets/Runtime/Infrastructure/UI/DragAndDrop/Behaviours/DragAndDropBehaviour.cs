using System.Collections.Generic;
using System.Linq;
using Runtime.Infrastructure.Models;
using Runtime.Infrastructure.UI.DragAndDrop.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Infrastructure.UI.DragAndDrop.Behaviours
{
    public abstract class DragAndDropBehaviour : MonoBehaviour, 
        IBeginDragHandler, 
        IEndDragHandler, 
        IDragHandler
    {
        [SerializeField] private Camera canvasCamara;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        protected ListenableValue<bool> IsDraggingValue { get; private set; } = new();
        protected RectTransform RectTransform
        {
            get
            {
                if (_cachedTransform == null)
                    _cachedTransform = GetComponent<RectTransform>();

                return _cachedTransform;
            }
        }
        
        private RectTransform _dragChild;
        private RectTransform _cachedTransform;
        private readonly List<RaycastResult> _raycastBuffer = new(16);
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            
            _dragChild = CreateDraggableChild(RectTransform);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, eventData.position, canvasCamara,
                    out var localPoint) == false)
                return;
            
            IsDraggingValue.Value = true;
            _dragChild.anchoredPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(_dragChild != null)
                Destroy(_dragChild.gameObject);
            
            _raycastBuffer.Clear();

            graphicRaycaster.Raycast(eventData, _raycastBuffer);
            if (_raycastBuffer.Count > 0)
            {
                var objects = _raycastBuffer
                    .Select(raycastResult => raycastResult.gameObject.GetComponent<IDraggableTarget>())
                    .Where(go => go != null).ToArray();


                foreach (var target in objects)
                {
                    target.OnDragEnd();
                    OnDragTarget(target);
                }
            }

            IsDraggingValue.Value = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDraggingValue.Value == false)
            {
                Debug.LogError("Drag status is false");
                return;
            }

            if (_dragChild == null)
            {
                Debug.LogError("Drag child is null");
                return;
            }


            _dragChild.anchoredPosition += eventData.delta;
        }


        protected abstract void OnDragTarget(IDraggableTarget targets);
        protected abstract RectTransform CreateDraggableChild(RectTransform parent);
    }
}