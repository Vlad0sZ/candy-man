using System;
using System.Collections.Generic;
using Runtime.GameEngine.Behaviours.Child;
using Runtime.GameEngine.Data;
using Runtime.Infrastructure.FactoryBase;
using Runtime.Infrastructure.RandomCore.Impl;
using Runtime.Infrastructure.RandomCore.Interfaces;
using Runtime.Infrastructure.Utility;
using UnityEngine;

namespace Runtime.GameEngine.Factories
{
    public class CharacterFactory : AbstractFactoryByData<ChildInformation, Child>
    {
        [SerializeField] private Child[] childrenPrefabs;
        
        private IRandom _random;
        private Stack<Child> _childrenStack;

        private void Awake()
        {
            _random = new ManualRandom();
            _childrenStack = GetShuffleStack();
        }


        public override Child Instantiate(ChildInformation information, Transform parent, bool worldPositionStays)
        {
            if (_childrenStack == null || _childrenStack.Count == 0)
                _childrenStack = GetShuffleStack();

            var childPrefab = _childrenStack.Pop();
            var child = Instantiate(childPrefab, parent, worldPositionStays);
            child.Init(information);

            return child;
        }

        private Stack<Child> GetShuffleStack() => 
            childrenPrefabs.ToShuffleStack(_random);
    }
}