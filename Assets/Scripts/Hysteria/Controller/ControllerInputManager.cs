using System;
using UnityEngine;

namespace Hysteria.Controller
{
    public class ControllerInputManager : ControllerComponent
    {
        private ControllerInputMap _inputMap;

        private void OnEnable()
        {
            EnableInputs();
        }

        private void OnDisable()
        {
            DisableInputs();
        }

        private void Awake()
        {
            _inputMap = new ControllerInputMap();
        }

        public ControllerInputMap GetMap()
        {
            if (_inputMap is null)
                _inputMap = new ControllerInputMap();
            return _inputMap;
        }

        public void EnableInputs()
        {
            _inputMap.Enable();
        }

        public void DisableInputs()
        {
            _inputMap.Disable();
        }
    }
}