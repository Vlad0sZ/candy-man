using Runtime.GameEngine.Models;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    [RequireComponent(typeof(Collider2D))]
    public class CandyBag : MonoBehaviour, IDraggableTarget<CandyModel>
    {
        public void OnDragEnd(CandyModel data) => 
            Debug.LogError($"receive {data.candyType}");
    }
}