using System;
using Runtime.GameEngine.Data;
using Runtime.GameEngine.Factories;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.Layer;
using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours.Child
{
    public class ChildHolder : MonoBehaviour
    {
        [SerializeField] private CharacterFactory childrenFactory;
        [SerializeField] private GridObject parentObject;
        [SerializeField] private UnityEvent<int> onIncrement;

        public UnityEvent<int> OnIncrement => onIncrement;

        private IRandom _random;
        private int _childCount;
        private ChildGeneration _childGeneration;
        
        private IChild[] _buffer;
        private Transform _parent;
        private ChildBehaviourFactory _behaviourFactory;

        public void Init(IRandom random, int childCount, ChildGeneration generation)
        {
            _random = random;
            _childCount = childCount;
            _childGeneration = generation;
            CreateChildren();
        }

        public void Deactivate()
        {
            if(_buffer == null)
                return;

            foreach (var child in _buffer) 
                child?.DestroyChild();

            _buffer = null;
        }
        
        private void CreateChildren()
        {
            _parent = parentObject.transform;
            _behaviourFactory = new ChildBehaviourFactory(_random);

            _buffer = new IChild[_childCount];
            for (var index = 0; index < _childCount; index++)
                CreateChildAt(index);
        }

        private void CreateChildAt(int index)
        {
            if(_buffer == null)
                return;
            
            var childInformation = new ChildInformation()
            {
                CandyBehaviour = _behaviourFactory.GetNext(),
                MaxCandyInABag = _childGeneration.GetMinMaxCandyInABag(_random),
                DisappointmentIncrement = _childGeneration.GetDisappointmentIncrement(_random),
                StressDecrement = _childGeneration.GetStressDecrement(_random),
                StressIncrement = _childGeneration.GetStressIncrement(_random),
                Timer =  _childGeneration.GetChildTimer(_random),
            };
            
            var child = childrenFactory.Instantiate(childInformation, _parent, false);
            child.SetHierarchyIndex(index);
            _buffer[index] = child;

            child.OnChildStatusChanged.AddListener(ChildStatusChanged);
        }

        private void ChildStatusChanged(IChild child)
        {
            var childIndex = Array.FindIndex(_buffer, c => c == child);
            if (childIndex < 0)
                throw new Exception("Child index in buffer was less than zero");

            child.DestroyChild();

            int value = child.ChildStatus == ChildStatus.Sad ? child.StressIncrement : -child.StressDecrement;
            onIncrement?.Invoke(value);
            
            CreateChildAt(childIndex);
        }
    }
}