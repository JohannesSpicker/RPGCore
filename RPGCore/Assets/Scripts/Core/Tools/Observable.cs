using System;

namespace Core.Tools
{
    public class Observable<T>
    {
        private event Action<T> OnValueChanged;
        private T                       value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(Value);
            }
        }

        public void Subscribe(Action<T>   observer) => OnValueChanged += observer;
        public void Unsubscribe(Action<T> observer) => OnValueChanged -= observer;
    }

    public class Observer<T>
    {
        private readonly Action<T>     action;
        private readonly Observable<T> observable;

        public Observer(Observable<T> observable, Action<T> action)
        {
            this.action     = action;
            this.observable = observable;

            observable.Subscribe(action);
        }

        public void Deconstruct() => observable.Unsubscribe(action);
    }
}