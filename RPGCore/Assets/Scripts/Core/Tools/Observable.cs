using System;

namespace Core.Tools
{
    public class Observable<T> where T : class
    {
        private T              myField;
        private Action<T>      onValueChanged;
        private IObservable<T> observable;
    }

    public class Observer<T> : IObserver<T>
    {
        public void OnCompleted()            => throw new NotImplementedException();
        public void OnError(Exception error) => throw new NotImplementedException();
        public void OnNext(T          value) => throw new NotImplementedException();
    }

    public class MyObservable<T> : IObservable<T>
    {
        public IDisposable Subscribe(IObserver<T> observer) => throw new NotImplementedException();
    }
}