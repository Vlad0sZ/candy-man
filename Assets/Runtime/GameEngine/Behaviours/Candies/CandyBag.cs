using Runtime.GameEngine.Models;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours.Candies
{
    [RequireComponent(typeof(Collider2D))]
    public class CandyBag : MonoBehaviour, IDraggableTarget<CandyType>, IDraggableTarget<Coal>
    {
        [SerializeField]
        private UnityEvent<CandyType> onCandyInBag;
        
        [SerializeField]
        private UnityEvent onCoalInBag;

        
        
        public UnityEvent<CandyType> OnCandyInBag => onCandyInBag;
        public UnityEvent OnCoalInBag => onCoalInBag;

        
        
        public void OnDragEnd(CandyType data) => 
            onCandyInBag?.Invoke(data);

        public void OnDragEnd(Coal data) => 
            onCoalInBag?.Invoke();
    }
}