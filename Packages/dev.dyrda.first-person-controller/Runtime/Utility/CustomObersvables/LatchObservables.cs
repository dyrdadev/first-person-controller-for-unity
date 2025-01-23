using System;
using System.Diagnostics;
using R3;
using R3.Triggers;
using Random = UnityEngine.Random;

namespace DyrdaDev.FirstPersonController
{
    public static class LatchObservables
    {
        public static Observable<bool> Latch(Observable<Unit> tick, Observable<Unit> latchTrue, bool initialValue)
        {
            // This custom observable is based on the "ReactiveX and Unity" tutorial series by Tyler Coles.
            // https://ornithoptergames.com/reactivex-and-unity3d-part-3/

            return Observable.Create<bool>(observer =>
            {
                var state = initialValue;

                // Whenever latch fires, state is set to true.
                var latchSubscription = latchTrue.Subscribe(_ => state = true);

                // Whenever tick fires, emit the current value and reset state.
                var tickSubscription = tick.Subscribe(_ =>
                    {
                        observer.OnNext(state);
                        state = false;
                    },
                    observer.OnErrorResume,
                    observer.OnCompleted);

                return Disposable.Create(() =>
                {
                    latchSubscription.Dispose();
                    tickSubscription.Dispose();
                });
            });
        }
    }
}