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
        [SerializeField] protected float dragForce = 0.05f;
        [SerializeField] protected float accelerationFactor = 2f;
        [SerializeField] protected Transform cameraTransform;

        private Vector3 movement;
        private Vector3 lastNonZeroMovement;
        private float verticalRotation = 0f;
        private Vector2 _directInput, _smoothInput, _directLookInput;
        
        protected void Start()
        {
            // Ensure the Rigidbody is set up correctly
            RB.freezeRotation = true;
            // RB.constraints = RigidbodyConstraints.FreezeRotationZ;

            if (!cameraTransform)
            {
                cameraTransform = Camera.main.transform;
            }

            ToggleMode(false);
        }

        public void RegisterActions()
        {
            InputManager.GetMap().Topdown.Movement.performed += OnPerformMovement;
            InputManager.GetMap().Topdown.Movement.canceled += OnCancelMovement;
            InputManager.GetMap().FP.Movement.performed += OnPerformMovement;
            InputManager.GetMap().FP.Movement.canceled += OnCancelMovement;
            InputManager.GetMap().FP.CameraLook.performed += OnPerformCameraLook;
            InputManager.GetMap().FP.CameraLook.canceled += OnCancelCameraLook;
        }

        public void UnregisterActions()
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
            float horizontalInput = _directInput.x;
            float verticalInput = _directInput.y;

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
            // TODO Remove Debug Code In production
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
            
            // Debug.Log(_smoothInput);
        }

        private void MoveCharacter()
        {
            Vector3 flatVelocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);

            // Apply force only when there's input
            if (movement != Vector3.zero)
            {
                // Calculate the force needed to reach max speed
                Vector3 targetVelocity = movement * maxSpeed;
                Vector3 velocityChange = targetVelocity - flatVelocity;
        
                // Increase the force for faster acceleration
                Vector3 force = velocityChange * (moveForce * accelerationFactor);

                // Remove the force clamping to allow for quicker acceleration
                RB.AddForce(force, ForceMode.Force);
            }
            else
            {
                // Apply drag when there's no input
                RB.AddForce(-flatVelocity * dragForce, ForceMode.VelocityChange);
            }

            // Limit max speed
            if (flatVelocity.magnitude > maxSpeed)
            {
                flatVelocity = flatVelocity.normalized * maxSpeed;
                RB.velocity = new Vector3(flatVelocity.x, RB.velocity.y, flatVelocity.z);
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
            ToggleMode(!isFirstPersonMode);
        }
        
        public void ToggleMode(bool value)
        {
            isFirstPersonMode = value;
            
            UpdateCursorState();

            if (isFirstPersonMode)
            {
                verticalRotation = 0f;
                cameraTransform.localRotation = Quaternion.identity;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
            }
            
            // RB.useGravity = isFirstPersonMode;
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
            // Debug.Log("In " + _directInput);
        }

        private void OnCancelMovement(InputAction.CallbackContext ctx)
        {
            _directInput = Vector2.zero;
        }

        private void OnPerformCameraLook(InputAction.CallbackContext ctx)
        {
            _directLookInput = ctx.ReadValue<Vector2>();
            // Debug.Log(_directLookInput);
        }

        private void OnCancelCameraLook(InputAction.CallbackContext ctx)
        {
            _directLookInput = Vector2.zero;
        }
        #endregion
    }
}
