using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hysteria.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] protected bool isFirstPersonMode = false;
        public ControllerMovement Movement;
        public ControllerInputManager InputManager;
        public ControllerInteractionManager InteractionManager;
        public Rigidbody RB;
        internal Rigidbody _firstPersonObjectRB;
        internal CinemachineVirtualCamera _firstPersonCamera;

        public void RegisterActions()
        {
            InputManager.GetMap().Topdown.Interact.performed += OnPerformInteract;
            InputManager.GetMap().FP.Interact.performed += OnPerformInteract;

            InputManager.GetMap().Topdown.LMB.performed += OnPerformLMB;
        }
        
        public void UnregisterActions()
        {
            InputManager.GetMap().Topdown.Interact.performed -= OnPerformInteract;
            InputManager.GetMap().FP.Interact.performed -= OnPerformInteract;

            InputManager.GetMap().Topdown.LMB.performed -= OnPerformLMB;
        }

        public void ToggleMode()
        {
            isFirstPersonMode = !isFirstPersonMode;
            Movement.ToggleMode(isFirstPersonMode);
            
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
        }

        public bool IsFirstPerson() => isFirstPersonMode;

        #region Input Action Callbacks

        private void OnPerformInteract(InputAction.CallbackContext ctx)
        {
            InteractionManager.InteractSelectedObject();
        }

        private void OnPerformLMB(InputAction.CallbackContext ctx)
        {
            
        }

        #endregion

        public void SwitchToFirstPerson(GameObject obj)
        {
            _firstPersonObjectRB = obj.GetComponent<Rigidbody>();
            _firstPersonCamera = obj.GetComponentInChildren<CinemachineVirtualCamera>();
            
            ToggleMode();
        }

        public void ExitFirstPerson()
        {
            ToggleMode();
            _firstPersonObjectRB = null;
            _firstPersonCamera = null;
        }
    }
}