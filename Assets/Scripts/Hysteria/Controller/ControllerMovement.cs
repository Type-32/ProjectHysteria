using System;
using Cinemachine;
using Hysteria.MenuControls;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Hysteria.Controller
{
    [RequireComponent(typeof(ControllerInputManager))]
    public class ControllerMovement : ControllerComponent
    {
        [SerializeField] protected bool isFirstPersonMode = false;
        [SerializeField] protected float maxSpeed = 5f;
        [SerializeField] protected float rotationSpeed = 10f;
        [SerializeField, MinMaxSlider(0, 200)] protected float mouseSensitivity = 100f;
        [SerializeField] protected CinemachineVirtualCamera firstPersonCamera;
        [SerializeField] protected CinemachineVirtualCamera topdownCamera;
        protected new Camera camera;

        private Vector3 movement, lastNonZeroMovement;
        private float verticalRotation = 0f;
        private Vector2 _directInput, _smoothInput, _directLookInput;
        
        protected void Start()
        {
            // Ensure the Rigidbody is set up correctly
            // RB.freezeRotation = true;
            // RB.constraints = RigidbodyConstraints.FreezeRotationZ;

            if (!camera)
            {
                camera = Camera.main;
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
            if (PauseBehaviour.Instance.Paused) return;
            
            _smoothInput = Vector2.Lerp(_smoothInput, _directInput, Time.deltaTime * 9);

            float horizontalInput = _smoothInput.x;
            float verticalInput = _smoothInput.y;

            // Calculate movement vector
            if (isFirstPersonMode)
            {
                movement = Controller._firstPersonObjectRB.transform.right * horizontalInput + Controller._firstPersonObjectRB.transform.forward * verticalInput;
            }
            else
            {
                movement = new Vector3(_smoothInput.x, 0f, _smoothInput.y);
            }

            // Handle rotation in first-person mode
            if (isFirstPersonMode)
            {
                // Debug.Log(_directLookInput);
                float mouseX = _directLookInput.x * mouseSensitivity;
                float mouseY = _directLookInput.y * mouseSensitivity;

                Controller._firstPersonObjectRB.transform.Rotate(0, mouseX, 0);

                verticalRotation -= mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
                firstPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
            else
            {
                RotateCharacter();
            }
        }

        private void FixedUpdate()
        {
            if (PauseBehaviour.Instance.Paused) return;
            MoveCharacter();
            // Debug.Log(_smoothInput);
        }

        private void MoveCharacter()
        {
            if (_directInput.Equals(Vector2.zero) && (_smoothInput.x <= 0.05 || _smoothInput.y <= 0.05))
                _smoothInput = Vector2.zero;
            
            Vector3 targetVelocity = movement * maxSpeed;

            if (isFirstPersonMode)
            {
                targetVelocity.y = Controller._firstPersonObjectRB.velocity.y;
                Controller._firstPersonObjectRB.velocity = targetVelocity;
            }
            else
            {
                targetVelocity.y = RB.velocity.y;
                RB.velocity = targetVelocity;
            }
        }

        private void RotateCharacter()
        {
            if (movement.magnitude > 0.1f)  // Only rotate if there's significant movement
            {
                lastNonZeroMovement = movement;  // Update lastNonZeroMovement here
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
                firstPersonCamera = Controller._firstPersonCamera;
                firstPersonCamera.transform.localRotation = Quaternion.identity;
                firstPersonCamera.Priority = 20;
                topdownCamera.Priority = 10;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
                firstPersonCamera.Priority = 10;
                topdownCamera.Priority = 20;
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
            // if (!isFirstPersonMode)
            // {
            //     lastNonZeroMovement = movement;
            // }
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
