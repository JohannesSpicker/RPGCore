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

    public class Observer<T> : IObserver<T>
    {
        public void OnCompleted()            => throw new NotImplementedException();
        public void OnError(Exception error) => throw new NotImplementedException();
        public void OnNext(T          value) => throw new NotImplementedException();
    }

    public class SomeClass
    {
        private Observable<float> someFloat = new Observable<float>();

        private void somefunc()
        {
            someFloat.Subscribe(myAction);
        }
        
        private void myAction(float number){}
        
        
        
    }
}