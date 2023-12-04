using System;
using System.Linq;
using Runtime.GameEngine.Behaviours.Bubbles;
using Runtime.GameEngine.Behaviours.Child.CandyBehaviours;
using Runtime.GameEngine.Data;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Impl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.GameEngine.Behaviours.Child
{
    public class Child : MonoBehaviour, IChild
    {
        private const int MaxStress = 100;


        [Header("Visual")] 
        [SerializeField] 
        private SpriteRenderer childFace;
        
        [SerializeField] 
        private ChildFaceByStress[] faces;

        [Header("UI")] 
        [SerializeField] 
        private Canvas worldCanvas;
        
        [SerializeField] 
        private Image childStressBar;
        
        [SerializeField] 
        private Bubble childBubble;

        [Header("Logic")]

        [Range(10, 30)] 
        [SerializeField]
        private int stressIncrement;

        [Range(1, 10)] 
        [SerializeField]
        private int stressDecrement;

        [Range(10, 50)] 
        [SerializeField] 
        private int disappointmentIncrement;

        [SerializeField] 
        private UnityEvent<IChild> onChildStatusChanged = new();

        public UnityEvent<IChild> OnChildStatusChanged => onChildStatusChanged;

        private int _childStress;
        private ChildCandyBehaviour _candyBehaviour;
        private int _maxCandies;
        
        public int ChildStress
        {
            get => _childStress;
            private set
            {
                _childStress = Mathf.Clamp(value, 0, MaxStress);
                UpdateFace();
            }
        }

        public ChildStatus ChildStatus { get; private set; }
        public int StressIncrement => disappointmentIncrement;
        public int StressDecrement => Mathf.Max(0, ChildStress - stressDecrement * 2);

        public void DestroyChild()
        {
            if (this != null && gameObject != null)
                Destroy(gameObject);
        }


        public void Init(ChildInformation childInformation)
        {
            worldCanvas.worldCamera = childInformation.WorldCamera;
            _candyBehaviour = childInformation.CandyBehaviour;
            _maxCandies = childInformation.MaxCandyInABag;
        }


        private void Start() => 
            InitChild();

        [ContextMenu(nameof(InitChild))]
        private void InitChild()
        {
            _childStress = 0;
            UpdateFace();
            _candyBehaviour.Init(new UnityRandom(), _maxCandies, childBubble.Builder());
        }


        [ContextMenu(nameof(MakeHappy))]
        private void MakeHappy() => 
            UpdateStatus(ChildStatus.Happy);

        [ContextMenu(nameof(MakeStress))]
        private void MakeStress() => 
            UpdateStatus(ChildStatus.Sad);


        public void GiveCandy(CandyType candyType)
        {
            var status = _candyBehaviour.GetCandyStatus(candyType);

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
                    UpdateStatus(ChildStatus.Happy);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void UpdateStressStatus(int increment)
        {
            ChildStress += increment;

            if (ChildStress >= MaxStress)
                UpdateStatus(ChildStatus.Sad);
        }

        private void UpdateStatus(ChildStatus status)
        {
            ChildStatus = status;
            onChildStatusChanged?.Invoke(this);
        }


        private void UpdateFace()
        {
            var faceSprite = GetFaceSprite();
            if (faceSprite == null)
                throw new Exception($"Can not find face by stress {_childStress}");

            childFace.sprite = GetFaceSprite();
        }

        private Sprite GetFaceSprite() =>
            faces.FirstOrDefault(f => f.maxStress >= _childStress).face;
    }
}