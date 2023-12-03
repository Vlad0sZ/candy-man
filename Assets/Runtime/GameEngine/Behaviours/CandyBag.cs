using Runtime.GameEngine.Models;
using Runtime.Infrastructure.UI.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours
{
    public class CandyBag : MonoBehaviour, IDraggableTarget<CandyModel>
    {
        public void OnDragEnd(CandyModel data) => 
            Debug.LogError($"receive {data.candyType}");

        public void OnDragEnd() => 
            Debug.LogError($"receive any");
    }
}