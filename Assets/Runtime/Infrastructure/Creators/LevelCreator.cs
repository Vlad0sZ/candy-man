using System;
using Runtime.GameEngine.Behaviours.Candies;
using Runtime.GameEngine.Behaviours.Child;
using Runtime.GameEngine.Behaviours.UI;
using Runtime.GameEngine.Data;
using Runtime.Infrastructure.Menu;
using Runtime.Infrastructure.RandomCore.Impl;
using Runtime.Infrastructure.RandomCore.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Infrastructure.Creators
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private Timer levelTimer;
        [SerializeField] private StressBar stressBar;

        [SerializeField] private CandyHolder candyHolder;
        [SerializeField] private ChildHolder childHolder;

        public UnityEvent Win;
        public UnityEvent Lose;

        private IRandom _globalRandom;
        private LevelInfo _levelInfo;
        
        private void Start()
        {
            _levelInfo = LevelLoader.Instance.CurrentInfo;
            LoadLevelInfo(_levelInfo);
        }
        

        private void LoadLevelInfo(LevelInfo obj) => 
            LoadLevelInfo(new UnityRandom(), obj);

        public void LoadMenu() => 
            LevelLoader.Instance.UnloadLevel();

        public void ReloadCurrent() =>
            LevelLoader.Instance.LoadLevel(_levelInfo);

        public void LoadLevelInfo(IRandom random, LevelInfo levelInfo)
        {
            _globalRandom = random;

            candyHolder.Init(random, levelInfo.bowlsCount, levelInfo.hasCoalBagInLevel);
            childHolder.Init(random, levelInfo.activeChildrenCount, levelInfo.childGeneration);
            
            
            childHolder.OnIncrement.AddListener(stressBar.Increment);
            stressBar.OnProgress.AddListener(OnChildProgress);
            levelTimer.OnTimerEnd.AddListener(OnTimerEnd);

            levelTimer.StartTimer(levelInfo.levelTimeInSeconds);
            stressBar.Init(levelInfo.childrenStress);
        }

        private void OnTimerEnd()
        {
            DestroyLevel();
            Win?.Invoke();
        }

        private void OnChildProgress(float p)
        {
            if (p  < 1)
                return;
            
            DestroyLevel();
            Lose?.Invoke();
        }

        private void DestroyLevel()
        {
            childHolder.OnIncrement.RemoveListener(stressBar.Increment);
            stressBar.OnProgress.RemoveListener(OnChildProgress);
            levelTimer.OnTimerEnd.RemoveListener(OnTimerEnd);
            
            levelTimer.StopTimer();
            childHolder.Deactivate();
        }
    }
}