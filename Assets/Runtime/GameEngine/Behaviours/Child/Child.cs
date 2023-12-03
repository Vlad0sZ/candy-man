using System;
using System.Linq;
using Runtime.GameEngine.Behaviours.Candies;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.GameEngine.Behaviours.Child
{
    public class Child : MonoBehaviour
    {
        private const int MaxStress = 100;

        [SerializeField] private ChildCandyBehaviour candyBehaviour;
        [SerializeField] private SpriteRenderer childFace;
        [SerializeField] private Image childStressBar;
        [SerializeField] private ChildFaceByStress[] faces;

        [Range(10, 30)]
        [SerializeField] private int stressIncrement;

        [Range(1, 10)]
        [SerializeField] private int stressDecrement;
        
        [Range(10, 50)]
        [SerializeField] private int disappointmentIncrement;


        [Range(1, 5)]
        [SerializeField] private int maxCandiesInABag;
        
        private int childStress;


        public int ChildStress
        {
            get => childStress;
            private set
            {
                childStress = Mathf.Clamp(value, 0, MaxStress);
                UpdateFace();
            }
        }

        private void Start() => 
            InitChild();

        [ContextMenu(nameof(InitChild))]
        private void InitChild()
        {
            childStress = 0;
            UpdateFace();
            candyBehaviour.Init(new UnityRandom(), maxCandiesInABag);
        }


        public void GiveCandy(CandyType candyType)
        {
            var status = candyBehaviour.GetCandyStatus(candyType);

            switch (status)
            {
                // Правильный выбор конфеты
                case GiftStatus.NeedMoreCandies:
                    UpdateStressStatus(-stressDecrement);
                    break;
                    
                // Неправильный выбор конфеты
                case GiftStatus.TastelessCandy:
                    UpdateStressStatus(stressIncrement);
                    break;

                // достаточно конфет для ребенка
                case GiftStatus.EnoughCandy:
                    // TODO destroy this child
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void UpdateStressStatus(int increment)
        {
            ChildStress += increment;

            if (ChildStress >= MaxStress)
            {
                // TODO invoke child sad :(
            }
        }


        private void UpdateFace()
        {
            var faceSprite = GetFaceSprite();
            if (faceSprite == null)
                throw new Exception($"Can not find face by stress {childStress}");

            childFace.sprite = GetFaceSprite();
        }


        private Sprite GetFaceSprite() => 
            faces.FirstOrDefault(f => f.maxStress >= childStress).face;
    }
}