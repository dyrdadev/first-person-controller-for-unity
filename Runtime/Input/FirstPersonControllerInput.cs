using System;
using R3;
using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public abstract class FirstPersonControllerInput : MonoBehaviour
    {
        /// <summary>
        ///     Move axes in WASD / D-Pad style.
        ///     Interaction type: continuous axes.
        /// </summary>
        public abstract Observable<Vector2> Move { get; }

        /// <summary>
        ///     Jump button.
        ///     Interaction type: Trigger.
        /// </summary>
        public abstract Observable<Unit> Jump { get; }

        /// <summary>
        ///     Run button.
        ///     Interaction type: Toggle.
        /// </summary>
        public abstract ReadOnlyReactiveProperty<bool> Run { get; }

        /// <summary>
        ///     Look axes following the free look (mouse look) pattern.
        ///     Interaction type: continuous axes.
        /// </summary>
        public abstract Observable<Vector2> Look { get; }
    }
}