using System;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.DragAndDrop.Behaviours;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class CandyBowl : DragAndDrop2D<CandyModel>, ICandyBowl
    {
        [SerializeField] private Candy candyPrefab;
        public event Action<ICandyBowl> BowlEmpty;
        private Candy _candyInBowl;
        
        private CandyModel _currentCandy;
        private CandyModel _nextCandy;

        private void OnEnable() => 
            this.IsDragging.OnValueChanged += CandyTaken;

        private void OnDisable() => 
            this.IsDragging.OnValueChanged -= CandyTaken;
        

        public void PutCandy(CandyModel candy)
        {
            var candyObject = Instantiate(candyPrefab, transform, true);
            candyObject.Renderer.sprite = candy.candySprite;
            candyObject.CandyTransform.localPosition = Vector3.zero;
            _candyInBowl = candyObject;
            _nextCandy = candy;
        }

        private void CandyTaken(bool isTaken)
        {
            if (isTaken)
            {
                _currentCandy = _nextCandy;
                BowlEmpty?.Invoke(this);
            }
        }

        protected override Transform CreateDraggableChild(Transform parent)
        {
            if (_candyInBowl == null)
                BowlEmpty?.Invoke(this);

            Transform candyTransform = _candyInBowl.CandyTransform;
            candyTransform.SetParent(parent);
            return candyTransform;
        }

        protected override void OnDragTarget(IDraggableTarget<CandyModel> target) => 
            target.OnDragEnd(_currentCandy);
    }
}