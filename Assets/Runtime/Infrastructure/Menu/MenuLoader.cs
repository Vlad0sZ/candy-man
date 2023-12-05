using Runtime.GameEngine.Data;
using UnityEngine;

namespace Runtime.Infrastructure.Menu
{
    public class MenuLoader : MonoBehaviour
    {
        public void LoadLevel(LevelInfo levelInfo) => 
            LevelLoader.Instance.LoadLevel(levelInfo);
    }
}