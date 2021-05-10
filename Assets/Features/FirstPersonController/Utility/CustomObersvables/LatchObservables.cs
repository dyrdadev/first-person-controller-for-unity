using System;
using System.Diagnostics;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;


public static class LatchObservables
{
    public static IObservable<bool> Latch(IObservable<Unit> tick, IObservable<Unit> latchTrue, bool initialValue)
    {
        // This custom observable is based on the "ReactiveX and Unity" tutorial series by Tyler Coles.
        // https://ornithoptergames.com/reactivex-and-unity3d-part-3/

        return Observable.Create<bool>(observer =>
        {
            var state = initialValue;

            // Whenever latch fires, state is set to true.
            var latchSubscribtion = latchTrue.Subscribe(_ => state = true);

            // Whenever tick fires, emit the current value and reset state.
            var tickSubscribtion = tick.Subscribe(_ =>
                {
                    observer.OnNext(state);
                    state = false;
                },
                observer.OnError,
                observer.OnCompleted);

            return Disposable.Create(() =>
            {
                latchSubscribtion.Dispose();
                tickSubscribtion.Dispose();
            });
        });
    }
}