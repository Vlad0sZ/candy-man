using Runtime.GameEngine.Models;
using Runtime.Infrastructure.UI.DragAndDrop.Behaviours;
using Runtime.Infrastructure.UI.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours
{
    public class CandyBowl : DragAndDropBehaviour
    {
        [SerializeField] private CandyModel candyModel;
        [SerializeField] private Candy candyPrefab;

        protected override void OnDragTarget(IDraggableTarget draggableTarget)
        {
                if (draggableTarget is IDraggableTarget<CandyModel> t)
                    t.OnDragEnd(candyModel);
        }

        protected override RectTransform CreateDraggableChild(RectTransform parent)
        {
            var candy = Instantiate(candyPrefab, parent, false);
            candy.Renderer.sprite = candyModel.candySprite;
            return candy.RectTransform;
        }
    }
}