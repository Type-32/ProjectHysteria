using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hysteria.Controller
{
    public class ControllerInputManager : ControllerComponent
    {
        private ControllerInputMap _inputMap;
        private List<Action> registerActions = new List<Action>();
        private List<Action> unregisterActions = new List<Action>();

        protected void OnEnable()
        {
            if (_inputMap is null)
                _inputMap = new ControllerInputMap();
            
            EnableInputs();
            Controller.RegisterActions();
            Movement.RegisterActions();
            foreach(Action i in registerActions)
            {
                i.Invoke();
            }
        }

        protected void OnDisable()
        {
            if (_inputMap is null)
                _inputMap = new ControllerInputMap();

            DisableInputs();
            Controller.UnregisterActions();
            Movement.UnregisterActions();
            foreach(Action i in unregisterActions)
            {
                i.Invoke();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _inputMap = new ControllerInputMap();
        }

        public ControllerInputMap GetMap()
        {
            // if (_inputMap is null)
            //     _inputMap = new ControllerInputMap();
            return _inputMap;
        }

        public void EnableInputs()
        {
            _inputMap.Enable();
            _inputMap.Topdown.Enable();
            _inputMap.FP.Enable();
            
            Debug.Log("Enabled Inputs.");
        }

        public void DisableInputs()
        {
            _inputMap.Disable();
            _inputMap.Topdown.Disable();
            _inputMap.FP.Disable();
            Debug.Log("Disabled Inputs.");
        }
    }
}