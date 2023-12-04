using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.GameEngine.Models;
using UnityEngine;

namespace Runtime.GameEngine.Data
{
    [CreateAssetMenu(fileName = "Candy Sprites", menuName = "GameEngine/Data/Candies")]
    public class CandySprites : ScriptableObject
    {
        [SerializeField] private CandySprite[] sprites;

        private Dictionary<CandyType, Sprite> _sprites;

        public Sprite GetSprite(CandyType byType)
        {

            if (_sprites == null || _sprites.Count == 0)
                _sprites = sprites.ToDictionary(t => t.candyType, t => t.candySprite);


            return _sprites.TryGetValue(byType, out var sprite)
                ? sprite
                : throw new Exception($"Can not find any sprite by type {byType}");
        }


        [System.Serializable]
        private struct CandySprite
        {
            public CandyType candyType;

            public Sprite candySprite;
        }
    }
}