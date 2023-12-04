using System;
using Runtime.Infrastructure.RandomCore.Interfaces;

namespace Runtime.Infrastructure.RandomCore.Impl
{
    public class ManualRandom : IRandom
    {
        private const float FloatIncrement = 0.1f;

        private bool _previousBoolean;
        private int _previousInt;
        private float _previousFloat;

        public int Next() => 
            Next(0, int.MaxValue);

        public int Next(int min, int max)
        {
            if (_previousInt >= max || _previousInt < min) 
                _previousInt = min;

            return _previousInt++;
        }

        public int Next(int max) => 
            Next(0, max);

        public float NextSingle()
        {
            float f = _previousFloat;
            _previousFloat += FloatIncrement;

            if (Math.Abs(_previousFloat - 1.0f) < 0.001f)
                _previousFloat = 0;
                
            
            return f;
        }


        public bool NextBoolean()
        {
            _previousBoolean = !_previousBoolean;
            return _previousBoolean;
        }
    }
}