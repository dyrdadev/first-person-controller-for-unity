using System;
using System.Diagnostics;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;

namespace DyrdaDev.FirstPersonController
{
    public static class SelectItemObservables
    {
        /// <summary>
        /// Maps each value to a random item.
        /// </summary>
        /// <param name="eventObs"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IObservable<T> SelectRandom<T>(this IObservable<Unit> eventObs, T[] items)
        {
            if (items.Length == 0)
            {
                return Observable.Empty<T>();
            }

            return Observable.Create<T>(observer =>
            {
                var sub = eventObs.Subscribe(_ =>
                    {
                        // Select random item and emit it.
                        observer.OnNext(items[Random.Range(0, items.Length)]);
                    },
                    observer.OnError,
                    observer.OnCompleted);

                return Disposable.Create(() => sub.Dispose());
            });
        }

        /// <summary>
        /// Maps each value to an alternating random item. This means, that an item cannot be selected twice in a row.
        /// </summary>
        /// <param name="eventObs"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IObservable<T> SelectAlternating<T>(this IObservable<Unit> eventObs, T[] items)
        {
            switch (items.Length)
            {
                case 0:
                    return Observable.Empty<T>();
                case 1:
                    return eventObs.Select(_ => items[0]);
            }

            var lastIndex = -1;
            return Observable.Create<T>(observer =>
            {
                var sub = eventObs.Subscribe(_ =>
                    {
                        // Select any item except the one from last iteration to prevent selecting an
                        // item twice in a row. (In the first pass, the first element cannot occur
                        // since i is initialized with -1. But I guess that's okay.)
                        var i = Random.Range(0, items.Length - 1);
                        i = i >= lastIndex ? i + 1 : i;
                        lastIndex = i;

                        // Emit the selected value.
                        observer.OnNext(items[i]);
                    },
                    observer.OnError,
                    observer.OnCompleted);

                return Disposable.Create(() => sub.Dispose());
            });
        }
    }
}