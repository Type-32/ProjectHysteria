using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hysteria.Controller
{
    [RequireComponent(typeof(ControllerInputManager))]
    public class ControllerMovement : ControllerComponent
    {
        [SerializeField] protected bool isFirstPersonMode = false;
        [SerializeField] protected float moveForce = 10f;
        [SerializeField] protected float maxSpeed = 5f;
        [SerializeField] protected float rotationSpeed = 10f;
        [SerializeField] protected float mouseSensitivity = 2f;
        [SerializeField] protected Transform cameraTransform;

        private Rigidbody rb;
        private Vector3 movement;
        private Vector3 lastNonZeroMovement;
        private float verticalRotation = 0f;
        private Vector2 _directInput, _smoothInput, _directLookInput;
        
        void Start()
        {
            // Ensure the Rigidbody is set up correctly
            RB.freezeRotation = true;
            RB.constraints = RigidbodyConstraints.FreezeRotationZ;

            if (!cameraTransform)
            {
                cameraTransform = Camera.main.transform;
            }

            // Lock and hide cursor for first-person mode
            UpdateCursorState();
        }

        private void OnEnable()
        {
            InputManager.GetMap().Topdown.Movement.performed += OnPerformMovement;
            InputManager.GetMap().Topdown.Movement.canceled += OnCancelMovement;
            InputManager.GetMap().FP.Movement.performed += OnPerformMovement;
            InputManager.GetMap().FP.Movement.canceled += OnCancelMovement;
            InputManager.GetMap().FP.CameraLook.performed += OnPerformCameraLook;
            InputManager.GetMap().FP.CameraLook.canceled += OnCancelCameraLook;
        }

        private void OnDisable()
        {
            InputManager.GetMap().Topdown.Movement.performed -= OnPerformMovement;
            InputManager.GetMap().Topdown.Movement.canceled -= OnCancelMovement;
            InputManager.GetMap().FP.Movement.performed -= OnPerformMovement;
            InputManager.GetMap().FP.Movement.canceled -= OnCancelMovement;
            InputManager.GetMap().FP.CameraLook.performed -= OnPerformCameraLook;
            InputManager.GetMap().FP.CameraLook.canceled -= OnCancelCameraLook;
        }

        void Update()
        {
            // Get input
            float horizontalInput = _smoothInput.x;
            float verticalInput = _smoothInput.y;

            // Calculate movement vector
            if (isFirstPersonMode)
            {
                movement = transform.right * horizontalInput + transform.forward * verticalInput;
            }
            else
            {
                movement = new Vector3(horizontalInput, 0f, verticalInput);
            }
            movement = movement.normalized;

            // Store last non-zero movement for top-down rotation when stationary
            if (movement != Vector3.zero)
            {
                lastNonZeroMovement = movement;
            }

            // Handle mode switching
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleMode();
            }

            // Handle rotation in first-person mode
            if (isFirstPersonMode)
            {
                float mouseX = _directLookInput.x * mouseSensitivity;
                float mouseY = _directLookInput.y * mouseSensitivity;

                transform.Rotate(Vector3.up * mouseX);

                verticalRotation -= mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
                cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
        }

        private void FixedUpdate()
        {
            MoveCharacter();

            if (!isFirstPersonMode)
            {
                RotateCharacter();
            }
            
            Debug.Log(_smoothInput);
        }

        private void MoveCharacter()
        {
            _smoothInput = Vector2.Lerp(_smoothInput, _directInput, Time.fixedDeltaTime * 7);
            
            // Apply force for movement
            RB.AddForce(movement * moveForce, ForceMode.Force);

            // Limit maximum speed
            Vector3 horizontalVelocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
            if (horizontalVelocity.magnitude > maxSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
                RB.velocity = new Vector3(horizontalVelocity.x, RB.velocity.y, horizontalVelocity.z);
            }

            // Apply drag when no input to slow down
            if (movement == Vector3.zero)
            {
                RB.drag = 2f;
            }
            else
            {
                RB.drag = 0f;
            }
        }

        private void RotateCharacter()
        {
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        public void ToggleMode()
        {
            isFirstPersonMode = !isFirstPersonMode;
            
            UpdateCursorState();

            if (isFirstPersonMode)
            {
                verticalRotation = 0f;
                cameraTransform.localRotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            }
        }
        
        public void ToggleMode(bool value)
        {
            isFirstPersonMode = value;
            
            if(isFirstPersonMode)
            {
                InputManager.GetMap().Topdown.Disable();
                InputManager.GetMap().FP.Enable();
            }
            else
            {
                InputManager.GetMap().Topdown.Enable();
                InputManager.GetMap().FP.Disable();
            }
            
            UpdateCursorState();

            if (isFirstPersonMode)
            {
                verticalRotation = 0f;
                cameraTransform.localRotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            }
        }

        private void UpdateCursorState()
        {
            Cursor.lockState = isFirstPersonMode ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isFirstPersonMode;
        }

        #region Input Action Callbacks

        private void OnPerformMovement(InputAction.CallbackContext ctx)
        {
            _directInput = ctx.ReadValue<Vector2>();
        }

        private void OnCancelMovement(InputAction.CallbackContext ctx)
        {
            _directInput = Vector2.zero;
        }

        private void OnPerformCameraLook(InputAction.CallbackContext ctx)
        {
            _directLookInput = ctx.ReadValue<Vector2>();
        }

        private void OnCancelCameraLook(InputAction.CallbackContext ctx)
        {
            _directLookInput = Vector2.zero;
        }
        #endregion
    }
}
