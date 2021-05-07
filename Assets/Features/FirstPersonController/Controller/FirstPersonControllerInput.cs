using System;
using UniRx;
using UnityEngine;

public abstract class FirstPersonControllerInput : MonoBehaviour
{
    /// <summary>
    ///     Move axes in WASD / D-Pad style.
    ///     Interaction type: continuous axes.
    /// </summary>
    public abstract IObservable<Vector2> Move { get; }

    /// <summary>
    ///     Jump button.
    ///     Interaction type: Trigger.
    /// </summary>
    public abstract IObservable<Unit> Jump { get; }

    /// <summary>
    ///     Run button.
    ///     Interaction type: Toggle.
    /// </summary>
    public abstract ReadOnlyReactiveProperty<bool> Run { get; }

    /// <summary>
    ///     Look axes following the free look (mouse look) pattern.
    ///     Interaction type: continuous axes.
    /// </summary>
    public abstract IObservable<Vector2> Look { get; }
}
