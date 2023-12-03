using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Candies
{
    public class Candy : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        [SerializeField]
        private Transform selfTransform;


        public Transform CandyTransform => selfTransform;

        public SpriteRenderer Renderer => spriteRenderer;
    }
}