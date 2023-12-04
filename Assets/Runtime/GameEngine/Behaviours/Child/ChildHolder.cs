using System;
using Runtime.GameEngine.Data;
using Runtime.GameEngine.Factories;
using Runtime.GameEngine.Interfaces;
using Runtime.Infrastructure.RandomCore.Impl;
using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Child
{
    public class ChildHolder : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;

        [SerializeField] private Transform[] childrenPositions;

        [SerializeField] private CharacterFactory childrenFactory;

        [Min(0)] [SerializeField] private float minDistanceBetweenSpawns = 1f;

        [Min(0.1f)] [SerializeField] private float gizmosRadius = 0.1f;


        private IChild[] _buffer;
        private ChildBehaviourFactory _behaviourFactory;
        private IRandom _random;
            
        private void Start() =>
            CreateChildren();

        private void CreateChildren()
        {
            if (worldCamera == null) 
                worldCamera = Camera.main;

            _random = new UnityRandom();
            _behaviourFactory = new ChildBehaviourFactory(_random);

            _buffer = new IChild[childrenPositions.Length];
            for (var index = 0; index < childrenPositions.Length; index++)
                CreateChildAt(index);
        }

        private void CreateChildAt(int index)
        {
            var spawn = childrenPositions[index];
            if (spawn == null)
            {
                Debug.LogError($"Spawn at {index} is null");
                return;
            }
            
            var childInformation = new ChildInformation()
            {
                CandyBehaviour = _behaviourFactory.GetNext(),
                MaxCandyInABag = _random.Next(2,5), // TODO from level info
                WorldCamera = worldCamera,
            };
            
            var child = childrenFactory.Instantiate(childInformation, spawn, false);
            _buffer[index] = child;

            child.OnChildStatusChanged.AddListener(ChildStatusChanged);
        }

        private void ChildStatusChanged(IChild child)
        {
            var childIndex = Array.FindIndex(_buffer, c => c == child);
            if (childIndex < 0)
                throw new Exception("Child index in buffer was less than zero");

            child.DestroyChild();
            Debug.Log($"Child at destroyed with {child.ChildStatus}");

            CreateChildAt(childIndex);
        }

        private void OnDrawGizmos()
        {
            if (childrenPositions == null || childrenPositions.Length == 0)
                return;

            var total = childrenPositions.Length;
            var c = Gizmos.color;

            Vector3 prev = default;
            for (int i = 0; i < total; i++)
            {
                var t = childrenPositions[i];
                if (t == null)
                    continue;
                var current = t.position;
                Gizmos.color = Vector3.Distance(prev, current) > minDistanceBetweenSpawns ? Color.green : Color.red;
                Gizmos.DrawSphere(current, gizmosRadius);

                prev = t.position;
            }

            Gizmos.color = c;
        }
    }
}