using System;

namespace Runtime.Infrastructure.Models
{
    public class ListenableValue<T>
    {

        private T _value;


        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }


        public event Action<T> OnValueChanged;
    }
}