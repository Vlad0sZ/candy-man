using System.Collections.Generic;
using System.Linq;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class CandyHolder : MonoBehaviour
    {
        [SerializeField] private CandyModel[] candies;
        [SerializeField] private CandyBowl[] candyBowls;

        private Stack<CandyModel> _candyStack;


        private void Awake() => 
            _candyStack = ShuffleCandies();

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
            if (_candyStack == null || _candyStack.Count == 0)
                _candyStack = ShuffleCandies();

            var nextCandy = _candyStack.Pop();
            candyBowl.PutCandy(nextCandy);
        }


        private Stack<CandyModel> ShuffleCandies()
        {
            List<CandyModel> listCopy = candies.ToList();
            int n = listCopy.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (listCopy[k], listCopy[n]) = (listCopy[n], listCopy[k]);
            }

            return new Stack<CandyModel>(listCopy);
        }
    }
}