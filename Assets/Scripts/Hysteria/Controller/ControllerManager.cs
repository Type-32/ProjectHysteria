using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hysteria.Controller
{
    public class ControllerManager : ControllerComponent
    {
        [SerializeField] protected bool isFirstPersonMode = false;
        [SerializeField] private ControllerMovement Movement;

        private void OnEnable()
        {
            InputManager.GetMap().Topdown.Interact.performed += OnPerformInteract;
            InputManager.GetMap().FP.Interact.performed += OnPerformInteract;

            InputManager.GetMap().Topdown.LMB.performed += OnPerformLMB;
        }

        private void OnDisable()
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
    }
}