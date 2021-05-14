using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class InputActionBasedFirstPersonControllerInput : FirstPersonControllerInput
{
    public override IObservable<Vector2> Move => _move;
    private IObservable<Vector2> _move;
    
    public override IObservable<Vector2> Look => _look;
    private IObservable<Vector2> _look;
    
    public override IObservable<Unit> Jump
    {
        get { return null; }
    }
    public override ReadOnlyReactiveProperty<bool> Run
    {
        get { return null; }
    }
    
    [Header("Look Properties")]
    [SerializeField] private float lookSmoothingFactor = 14.0f;

    private FirstPersonInputAction _controls;
    
    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
    
    protected void Awake()
    {
        _controls = new FirstPersonInputAction();

        // Hide the mouse cursor and lock it in the game window.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Move:
        _move = this.UpdateAsObservable()
            .Select(_ => _controls.Character.Move.ReadValue<Vector2>());
        
        // Look:
        var smoothLookValue = new Vector2(0, 0);
        _look = this.UpdateAsObservable()
            .Select(_ =>
            {
                var rawLookValue = _controls.Character.Look.ReadValue<Vector2>();

                smoothLookValue = new Vector2(
                    Mathf.Lerp(smoothLookValue.x, rawLookValue.x, lookSmoothingFactor * Time.deltaTime),
                    Mathf.Lerp(smoothLookValue.y, rawLookValue.y, lookSmoothingFactor * Time.deltaTime)
                );

                return smoothLookValue;
            });
    }
}