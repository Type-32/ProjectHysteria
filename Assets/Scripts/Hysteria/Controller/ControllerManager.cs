using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hysteria.Controller
{
    public class ControllerManager : MonoBehaviour
    {
        [SerializeField] protected bool isFirstPersonMode = false;
        public ControllerMovement Movement;
        public ControllerInputManager InputManager;
        public Rigidbody RB;

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
            
        }

        private void OnPerformLMB(InputAction.CallbackContext ctx)
        {
            
        }

        #endregion
        
        #region Message Listeners
        
        
        
        #endregion
    }
}