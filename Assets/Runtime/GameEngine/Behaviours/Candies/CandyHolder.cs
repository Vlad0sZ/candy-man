using System;
using System.Collections.Generic;
using Runtime.GameEngine.Factories;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Impl;
using Runtime.Infrastructure.RandomCore.Interfaces;
using Runtime.Infrastructure.Utility;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class CandyHolder : MonoBehaviour
    {
        [SerializeField] private CandyBowl[] candyBowls;

        private Stack<CandyType> _candies;
        private IEnumerable<CandyType> _allCandies;
        private IRandom _stackShuffleRandom;
        
        private void Awake()
        {
            _stackShuffleRandom = new ManualRandom();
            _allCandies = EnumExtensions.GetAllValues<CandyType>();
        }

        private void OnEnable()
        {
            foreach (var bowl in candyBowls)
                bowl.BowlEmpty += UpdateCandyIn;
        }

        private void OnDisable()
        {
            foreach (var bowl in candyBowls)
                bowl.BowlEmpty -= UpdateCandyIn;
        }

        private void Start()
        {
            foreach (var bowl in candyBowls) 
                UpdateCandyIn(bowl);
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