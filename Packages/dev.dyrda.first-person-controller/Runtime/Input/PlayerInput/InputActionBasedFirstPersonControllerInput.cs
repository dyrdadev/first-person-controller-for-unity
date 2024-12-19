using System;
using R3;
using R3.Triggers;
using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public class InputActionBasedFirstPersonControllerInput : FirstPersonControllerInput
    {
        public override Observable<Vector2> Move
        {
            get { return null; }
        }
        public override Observable<Unit> Jump
        {
            get { return null; }
        }
        public override ReadOnlyReactiveProperty<bool> Run
        {
            get { return null; }
        }
        public override Observable<Vector2> Look
        {
            get { return null; }
        }
    }
}