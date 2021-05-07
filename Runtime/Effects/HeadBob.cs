using UniRx;
using UnityEngine;

namespace DyrdaDev.FirstPersonController
{
    public class HeadBob : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject characterSignalsInterfaceTarget;
        private ICharacterSignals _characterSignals;
        private Camera _camera;

        [Header("Bob Configuration")]
        [SerializeField] private float walkBobMagnitude = 0.05f;
        [SerializeField] private float runBobMagnitude = 0.10f;
        [SerializeField] private AnimationCurve bobCurve = new AnimationCurve(
            new Keyframe(0.00f, 0f),
            new Keyframe(0.25f, 1f),
            new Keyframe(0.50f, 0f),
            new Keyframe(0.75f, -1f),
            new Keyframe(1.00f, 0f));

        private Vector3 _initialCameraPosition;

        private void Awake()
        {
            _characterSignals = characterSignalsInterfaceTarget.GetComponent<ICharacterSignals>();
            _camera = GetComponent<Camera>();
            _initialCameraPosition = _camera.transform.localPosition;
        }

        private void Start()
        {
            var distance = 0f;
            _characterSignals.Moved.Subscribe(w =>
            {
                // Calculate stride progress.
                distance += w.magnitude;
                distance %= _characterSignals.StrideLength;

                // Evaluate the current camera bob.
                var magnitude = _characterSignals.IsRunning.Value ? runBobMagnitude : walkBobMagnitude;
                var deltaPosition = magnitude * bobCurve.Evaluate(distance / _characterSignals.StrideLength) *
                                    Vector3.up;

                _camera.transform.localPosition = _initialCameraPosition + deltaPosition;
            }).AddTo(this);
        }
    }
}