using EasyTransition;
using Runtime.GameEngine.Data;
using UnityEngine;

namespace Runtime.Infrastructure.Menu
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private TransitionManager transitionManager;
        [SerializeField] private TransitionSettings transition;
        public static  LevelLoader Instance { get; private set; }
        public LevelInfo CurrentInfo { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void LoadLevel(LevelInfo levelInfo)
        {
            CurrentInfo = levelInfo;
            transitionManager.Transition(1, transition, 0);
        }

        public void UnloadLevel()
        {
            CurrentInfo = null;
            transitionManager.Transition(0, transition, 1);
        }
    }
}