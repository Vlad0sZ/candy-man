using UnityEngine;

namespace Runtime.GameEngine.Behaviours.Child
{
    [System.Serializable]
    public struct ChildFaceByStress
    {
        public Sprite face;

        public Color color;
        
        [Range(0, 100)]
        public int maxStress;
    }
}