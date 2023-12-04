using Runtime.GameEngine.Data;
using Runtime.GameEngine.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.GameEngine.Behaviours.Bubbles
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private LayoutGroup layoutGroup;
        [SerializeField] private TMP_Text textPrefab;
        [SerializeField] private Image notPrefab;
        [SerializeField] private Image candyPrefab;
        [SerializeField] private CandySprites candySprites;
        
        private RectTransform _groupRect;

        private void Awake() => 
            _groupRect = layoutGroup.GetComponent<RectTransform>();
        
        public IBubbleBuilder Builder() => new BubbleBuilder(this);
        
        private void Clear()
        {
            for (int i = 1; i < _groupRect.childCount; i++) 
                Destroy(_groupRect.GetChild(i).gameObject);
        }

        private void AddNumber(int number)
        {
            var t = Instantiate(textPrefab, _groupRect, false);
            t.text = number.ToString();
        }

        private void AddCandy(CandyType candyType)
        {
            var s = candySprites.GetSprite(candyType);
            var image = Instantiate(candyPrefab, _groupRect, false);
            image.sprite = s;

        }

        private void AddNotCandy(CandyType candyType)
        {
            var s = candySprites.GetSprite(candyType);
            var image = Instantiate(candyPrefab, _groupRect, false);
            image.sprite = s;

            Instantiate(notPrefab, image.rectTransform, false);
        }
        
        
        private class BubbleBuilder : IBubbleBuilder
        {
            private readonly Bubble _bubble;
            
            public BubbleBuilder(Bubble bubble)
            {
                _bubble = bubble;
                _bubble.Clear();
            }
            
            public IBubbleBuilder AddCandiesCount(int count)
            {
               _bubble.AddNumber(count);
               return this;
            }

            public IBubbleBuilder AddCandySprite(CandyType candyType, bool notPrefab)
            {
                if(notPrefab)
                    _bubble.AddNotCandy(candyType);
                else
                    _bubble.AddCandy(candyType);

                return this;
            }            
        }


    }
}