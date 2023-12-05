using System;
using System.Collections.Generic;
using Runtime.GameEngine.Factories;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.Layer;
using Runtime.Infrastructure.RandomCore.Interfaces;
using Runtime.Infrastructure.Utility;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class CandyHolder : MonoBehaviour
    {
        [SerializeField] private BowlFactory bowlFactory;
        [SerializeField] private CoalFactory coalFactory;
        [SerializeField] private GridObject bowlParent;

        private CandyBowl[] _candyBowls;
        private Stack<CandyType> _candies;
        private IEnumerable<CandyType> _allCandies;
        private IRandom _stackShuffleRandom;
        private int _bowlCount;
        private Transform _bowlParent;

        public void Init(IRandom random, int bowlCount, bool hasCoalInLevel)
        {
            _allCandies = EnumExtensions.GetAllValues<CandyType>();
            _bowlParent = bowlParent.transform;
            
            _stackShuffleRandom = random;
            _bowlCount = bowlCount;
            _candyBowls = new CandyBowl[bowlCount];

            if (hasCoalInLevel)
                coalFactory.InstantiateNext(_bowlParent, true);
            
            CreateBowls();
        }

        private void CreateBowls()
        {

            for (int i = 0; i < _bowlCount; i++)
            {
                var bowl = bowlFactory.InstantiateNext(_bowlParent, true);
                _candyBowls[i] = bowl;
                bowl.BowlEmpty += UpdateCandyIn;
                UpdateCandyIn(bowl);
            }
        }
        
        private void OnEnable()
        {
            if (_candyBowls == null) 
                return;
            
            foreach (var bowl in _candyBowls)
                bowl.BowlEmpty += UpdateCandyIn;
        }

        private void OnDisable()
        {
            if (_candyBowls == null) 
                return;

            foreach (var bowl in _candyBowls) 
                bowl.BowlEmpty -= UpdateCandyIn;
        }

        private void OnDestroy()
        {
            if (_candyBowls == null)
                return;

            foreach (var bowl in _candyBowls)
                if (bowl)
                    Destroy(bowl.gameObject);


            _candyBowls = null;
        }

        private void UpdateCandyIn(ICandyBowl candyBowl)
        {
            if (_candies == null || _candies.Count == 0)
                _candies = _allCandies.ToShuffleStack(_stackShuffleRandom);

            var nextCandy = _candies.Pop();
            candyBowl.PutCandy(nextCandy);
        }
    }
}