using System;
using System.Collections;
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

        private Coroutine _moveCandy;

        public void Init(CandyFactory factory, Camera dragAndDropCamera)
        {
            GameCamera = dragAndDropCamera;
            candyFactory = factory;
        }

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
            if (_moveCandy != null)
                StopCoroutine(_moveCandy);

            _moveCandy = StartCoroutine(MoveCandy());
        }

        private void CandyTaken(bool isTaken)
        {
            if (isTaken)
            {
                _currentCandy = _nextCandy;
                BowlEmpty?.Invoke(this);
            }
        }

        private IEnumerator MoveCandy()
        {
            float t = 0;
            Vector3 p = Vector3.up * 3f;
            while (t <= 1f)
            {
                t += Time.deltaTime;
                Vector3 lerp = Vector3.Lerp(Vector3.zero, p, t);
                _candyInBowl.CandyTransform.localPosition = lerp;
                yield return null;
            }

            _moveCandy = null;
        }

        protected override Transform CreateDraggableChild(Transform parent)
        {
            if (_candyInBowl == null)
                BowlEmpty?.Invoke(this);

            if (_moveCandy != null) StopCoroutine(_moveCandy);
            Transform candyTransform = _candyInBowl.CandyTransform;
            candyTransform.SetParent(parent);
            return candyTransform;
        }

        protected override void OnDragTarget(IDraggableTarget<CandyType> target) =>
            target.OnDragEnd(_currentCandy);
    }
}