using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class InputActionBasedFirstPersonControllerInput : FirstPersonControllerInput
{
    public override IObservable<Vector2> Move
    {
        get { return null; }
    }
    public override IObservable<Unit> Jump
    {
        get { return null; }
    }
    public override ReadOnlyReactiveProperty<bool> Run
    {
        get { return null; }
    }
    public override IObservable<Vector2> Look
    {
        get { return null; }
    }
}