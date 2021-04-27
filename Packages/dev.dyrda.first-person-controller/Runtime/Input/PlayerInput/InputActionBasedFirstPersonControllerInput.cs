using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public class InputActionBasedFirstPersonControllerInput : FirstPersonControllerInput
    {
        #region Controller Input Fields

        public override IObservable<Vector2> Move => _move;
        private IObservable<Vector2> _move;

        public override IObservable<Unit> Jump => _jump;
        private Subject<Unit> _jump;

        public override ReadOnlyReactiveProperty<bool> Run => _run;
        private ReadOnlyReactiveProperty<bool> _run;

        public override IObservable<Vector2> Look => _look;
        private IObservable<Vector2> _look;

        #endregion

        #region Configuration

        [Header("Look Properties")]
        [SerializeField] private float lookSmoothingFactor = 14.0f;

        private FirstPersonInputAction _controls;

        #endregion

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

            // Jump:
            _jump = new Subject<Unit>().AddTo(this);
            _controls.Character.Jump.performed += context => _jump.OnNext(Unit.Default);


            // Run:
            _run = this.UpdateAsObservable()
                .Select(_ => _controls.Character.Run.ReadValueAsObject() != null)
                .ToReadOnlyReactiveProperty();

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
}