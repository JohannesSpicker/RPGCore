using System;

namespace Core.Tools
{
    public class Observable<T>
    {
        private Action<T> onValueChanged;
        private T         value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                onValueChanged?.Invoke(Value);
            }
        }

        public void Subscribe(Action<T> observer) => onValueChanged += observer;
        public void Unsubscribe(Action<T> observer) => onValueChanged -= observer;
    }

    public class Observer<T>
    {
        private Action<T>     action;
        private Observable<T> observable;
        
        public Observer(Observable<T> observable, Action<T> action)
        {
            this.action     = action;
            this.observable = observable;
            
            observable.Subscribe(action);
        }
    }
}