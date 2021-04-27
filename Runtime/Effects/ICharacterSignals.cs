using System;
using UniRx;
using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public interface ICharacterSignals
    {
        /// <summary>
        ///     The stride length of the character.
        /// </summary>
        float StrideLength { get; }

        /// <summary>
        ///     A "Is the character running?" stream.
        /// </summary>
        ReactiveProperty<bool> IsRunning { get; }

        /// <summary>
        ///     A stream with the vectors the character has moved.
        /// </summary>
        IObservable<Vector3> moved { get; }

        /// <summary>
        ///     A stream with landed events. Triggered when the character switches form airborne to grounded.
        /// </summary>
        IObservable<Unit> Landed { get; }

        /// <summary>
        ///     A stream with jumped events. Triggered when the character starts to jump.
        /// </summary>
        IObservable<Unit> Jumped { get; }

        /// <summary>
        ///     A stream with stepped events. Triggered when the camera has moved one stride length.
        /// </summary>
        IObservable<Unit> Stepped { get; }
    }
}