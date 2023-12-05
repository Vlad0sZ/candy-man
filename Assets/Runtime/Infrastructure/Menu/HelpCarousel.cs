using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Infrastructure.Menu
{
    public class HelpCarousel : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] help;
        
        [SerializeField]
        private Image img;

        private int count;

        private void Start() => Carousel(0);


        public void Next() => Carousel(1);

        public void Prev() => Carousel(-1);

        private void Carousel(int increment)
        {
            
            int next = count + increment;
            if (next >= help.Length)
                count = 0;
            else if (next < 0)
                count = help.Length - 1;
            else
                count = next;

            img.sprite = help[count];
        }
    }
}