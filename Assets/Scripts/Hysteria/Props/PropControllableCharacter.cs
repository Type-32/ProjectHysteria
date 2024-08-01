using System;
using Hysteria.Controller;
using Hysteria.Interface;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hysteria.Props
{
    [RequireComponent(typeof(Rigidbody))]
    public class PropControllableCharacter : PropBehaviour
    {
        private ControllerManager _manager;

        private bool _inControl = false;

        private void Start()
        {
            _manager = FindObjectOfType<ControllerManager>();
        }

        public override void Interact()
        {
            base.Interact();
            if(!_inControl)
            {
                _manager.SwitchToFirstPerson(gameObject);
                _inControl = !_inControl;
                _manager.InputManager.GetMap().FP.ExitFirstPerson.performed += OnExitFirstPerson;
            }
        }

        private void OnExitFirstPerson(InputAction.CallbackContext ctx)
        {
            _manager.ExitFirstPerson();
            _inControl = !_inControl;
            _manager.InputManager.GetMap().FP.ExitFirstPerson.performed -= OnExitFirstPerson;
        }
    }
}