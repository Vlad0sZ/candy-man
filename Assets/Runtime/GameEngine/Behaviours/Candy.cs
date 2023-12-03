using UnityEngine;
using UnityEngine.UI;

namespace Runtime.GameEngine.Behaviours
{
    public class Candy : MonoBehaviour
    {
        [SerializeField]
        private Image imageRenderer;
        
        [SerializeField]
        private RectTransform selfTransform;


        public RectTransform RectTransform => selfTransform;

        public Image Renderer => imageRenderer;
    }
}