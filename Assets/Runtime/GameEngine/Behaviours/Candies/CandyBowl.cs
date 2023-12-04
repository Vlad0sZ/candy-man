using System;
using Runtime.GameEngine.Factories;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.DragAndDrop.Behaviours;
using Runtime.Infrastructure.DragAndDrop.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class CandyBowl : DragAndDrop2D<CandyType>, ICandyBowl
    {
        [SerializeField] private CandyFactory candyFactory;
        public event Action<ICandyBowl> BowlEmpty;
        private Candy _candyInBowl;
        
        private CandyType _currentCandy;
        private CandyType _nextCandy;

        private void OnEnable() => 
            this.IsDragging.OnValueChanged += CandyTaken;

        private void OnDisable() => 
            this.IsDragging.OnValueChanged -= CandyTaken;
        

        public void PutCandy(CandyType candy)
        {
            var candyObject = candyFactory.Instantiate(candy, transform, true);
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

        protected override void OnDragTarget(IDraggableTarget<CandyType> target) => 
            target.OnDragEnd(_currentCandy);
    }
}