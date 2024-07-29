using System;
using Cinemachine;
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
        protected Camera camera;

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
            
            float horizontalInput = _smoothInput.x;
            float verticalInput = _smoothInput.y;

            // Calculate movement vector
            if (isFirstPersonMode)
            {
                movement = transform.right * horizontalInput + transform.forward * verticalInput;
            }
            else
            {
                movement = new Vector3(_smoothInput.x, 0f, _smoothInput.y);
            }

            // Store last non-zero movement for top-down rotation when stationary
            if (!_directInput.Equals(Vector2.zero))
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

                transform.Rotate(0, mouseX, 0);

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
            MoveCharacter();
            // Debug.Log(_smoothInput);
        }

        private void MoveCharacter()
        {
            _smoothInput = Vector2.Lerp(_smoothInput, _directInput, Time.fixedDeltaTime * 9);

            if (_directInput.Equals(Vector2.zero) && (_smoothInput.x <= 0.05 || _smoothInput.y <= 0.05))
                _smoothInput = Vector2.zero;
            
            Vector3 targetVelocity = movement * maxSpeed;

            targetVelocity.y = RB.velocity.y;

            RB.velocity = targetVelocity;
        }

        private void RotateCharacter()
        {
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lastNonZeroMovement, Vector3.up);
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
