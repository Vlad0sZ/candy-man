using Runtime.GameEngine.Models;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours.Candies
{
    [RequireComponent(typeof(Collider2D))]
    public class CandyBag : MonoBehaviour, IDraggableTarget<CandyType>
    {
        [SerializeField]
        private UnityEvent<CandyType> onCandyInBag;
        public UnityEvent<CandyType> OnCandyInBag => onCandyInBag;

        public void OnDragEnd(CandyType data) => 
            onCandyInBag?.Invoke(data);
    }
}