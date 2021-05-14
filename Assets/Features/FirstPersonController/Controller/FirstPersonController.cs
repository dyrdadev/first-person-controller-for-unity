using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


/// <summary>
///     Controller that handles the character controls and camera controls of the first person player.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FirstPersonControllerInput firstPersonControllerInput;
    private CharacterController _characterController;
    private Camera _camera;
    
    [Header("Locomotion Properties")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float stickToGroundForceMagnitude = 5f;
    
    [Header("Look Properties")]
    [Range(-90, 0)] [SerializeField] private float minViewAngle = -60f;
    [Range(0, 90)] [SerializeField] private float maxViewAngle = 60f;
    
    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        HandleLocomotion();
        HandleLook();
    }
    
    private void HandleLocomotion()
    {
        // Ensures the first frame counts as "grounded".
        _characterController.Move(-stickToGroundForceMagnitude * transform.up);
        
        // Handle move:
        firstPersonControllerInput.Move
            .Where(i => i != Vector2.zero)
            .Subscribe(i =>
            {
                var wasGrounded = _characterController.isGrounded;
                
                // Vertical movement:
                var verticalVelocity = 0f;
                
                // Horizontal movement:
                var currentSpeed = walkSpeed;
                var horizontalVelocity = i * currentSpeed; //Calculate velocity (direction * speed).

                // Combine horizontal and vertical movement.
                var characterVelocity = transform.TransformVector(new Vector3(
                    horizontalVelocity.x,
                    verticalVelocity,
                    horizontalVelocity.y));

                // Apply movement.
                var distance = characterVelocity * Time.deltaTime;
                _characterController.Move(distance);
            }).AddTo(this);
    }

    private void HandleLook()
    {
        firstPersonControllerInput.Look
            .Where(v => v != Vector2.zero)
            .Subscribe(inputLook =>
            {
                // Translate 2D mouse input into euler angle rotations.

                // Horizontal look with rotation around the vertical axis, where + means clockwise.
                var horizontalLook = inputLook.x * Vector3.up * Time.deltaTime;
                transform.localRotation *= Quaternion.Euler(horizontalLook);

                // Vertical look with rotation around the horizontal axis, where + means upwards.
                var verticalLook = inputLook.y * Vector3.left * Time.deltaTime;
                var newQ = _camera.transform.localRotation * Quaternion.Euler(verticalLook);
                
                _camera.transform.localRotation =
                    RotationTools.ClampRotationAroundXAxis(newQ, -maxViewAngle, -minViewAngle);
            }).AddTo(this);
    }
}
