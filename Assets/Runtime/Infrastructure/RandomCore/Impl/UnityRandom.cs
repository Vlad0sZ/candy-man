using Runtime.Infrastructure.RandomCore.Interfaces;

namespace Runtime.Infrastructure.RandomCore.Impl
{
    public class UnityRandom : IRandom
    {
        public int Next() =>
            Next(0, int.MaxValue - 1);

        public int Next(int min, int max) => 
            UnityEngine.Random.Range(min, max);

        public int Next(int max) => 
            Next(0, max);

        public float NextSingle() => 
            UnityEngine.Random.value;

        public bool NextBoolean() => 
            NextSingle() >= 0.5f;
    }
}