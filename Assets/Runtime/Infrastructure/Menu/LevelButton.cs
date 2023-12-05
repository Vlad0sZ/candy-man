using System;
using Runtime.GameEngine.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Infrastructure.Menu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private GameObject closedObject;
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private bool opened;

        public UnityEvent<LevelInfo> OnLevelLoad;


        private void OnValidate() => 
            ActivateLock();

        private void Start()
        {
            if (levelInfo == null) 
                Opened = false;

            ActivateLock();
            tmpText.text = opened ? levelInfo.id.ToString() : string.Empty;
        }

        public bool Opened
        {
            get => opened;

            set
            {
                opened = value;
                ActivateLock();
            }
        }

        private void ActivateLock()
        {
            if (closedObject)
                closedObject.SetActive(!opened);
        }

        public void LoadLevel() =>
            OnLevelLoad?.Invoke(levelInfo);
    }
}