using System;
using System.Linq;
using Runtime.GameEngine.Behaviours.Bubbles;
using Runtime.GameEngine.Behaviours.Child.CandyBehaviours;
using Runtime.GameEngine.Behaviours.UI;
using Runtime.GameEngine.Data;
using Runtime.GameEngine.Interfaces;
using Runtime.GameEngine.Models;
using Runtime.Infrastructure.RandomCore.Impl;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameEngine.Behaviours.Child
{
    public class Child : MonoBehaviour, IChild
    {
        private const int MaxStress = 100;


        [Header("Visual")] 
        [SerializeField] 
        private SpriteRenderer childFace;

        [SerializeField] 
        private SpriteRenderer childStressBar;

        [SerializeField] 
        private ChildFaceByStress[] faces;

        [Header("UI")] 
        [SerializeField] 
        private Canvas worldCanvas;

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

        [SerializeField] private float childTime;

        [SerializeField] 
        private Timer childTimer;

        [SerializeField] 
        private UnityEvent<IChild> onChildStatusChanged = new();

        [SerializeField] 
        private UnityEvent<float> onChildProgress = new();

        public UnityEvent<IChild> OnChildStatusChanged => onChildStatusChanged;

        public UnityEvent<float> OnChildProgress => onChildProgress;

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

        public void SetHierarchyIndex(int index) => 
            transform.SetSiblingIndex(index);

        public void DestroyChild()
        {
            if (this != null && gameObject != null)
                Destroy(gameObject);
        }


        public void Init(ChildInformation childInformation, Camera worldCamera)
        {
            _candyBehaviour = childInformation.CandyBehaviour;
            _maxCandies = childInformation.MaxCandyInABag;
            disappointmentIncrement = childInformation.DisappointmentIncrement;
            stressIncrement = childInformation.StressIncrement;
            stressDecrement = childInformation.StressDecrement;
            childTime = childInformation.Timer;

            worldCanvas.worldCamera = worldCamera;
        }

        private void Start() => 
            InitChild();

        [ContextMenu(nameof(InitChild))]
        private void InitChild()
        {
            _childStress = 0;
            UpdateFace();
            
            OnChildProgress?.Invoke(0);
            _candyBehaviour.Init(new UnityRandom(), _maxCandies, childBubble.Builder());
            _candyBehaviour.OnProgress += MakeProgress;
            childTimer.OnTimerEnd.AddListener(MakeStress);
            childTimer.StartTimer(childTime);
        }

        private void MakeProgress(float progress) => 
            OnChildProgress?.Invoke(progress);


        [ContextMenu(nameof(MakeHappy))]
        private void MakeHappy() => 
            UpdateStatus(ChildStatus.Happy);

        [ContextMenu(nameof(MakeStress))]
        private void MakeStress() => 
            UpdateStatus(ChildStatus.Sad);

        public void GiveCoal() => 
            MakeStress();

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
            childTimer.StopTimer();
            onChildStatusChanged?.Invoke(this);
        }


        private void UpdateFace()
        {
            var stress = GetFaceByStress();
            
            if (stress.face == null)
                throw new Exception($"Can not find face by stress {_childStress}");

            childFace.sprite = stress.face;
            childStressBar.color = stress.color;
        }

        private ChildFaceByStress GetFaceByStress() =>
            faces.FirstOrDefault(f => f.maxStress >= _childStress);
    }
}