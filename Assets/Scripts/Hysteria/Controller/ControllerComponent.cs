using System;
using UnityEngine;

namespace Hysteria.Controller
{
    public class ControllerComponent : MonoBehaviour
    {
        protected ControllerManager Controller;
        protected ControllerInputManager InputManager;
        protected Rigidbody RB;

        private void Awake()
        {
            Controller = GetComponent<ControllerManager>();
            InputManager = GetComponent<ControllerInputManager>();
            RB = GetComponent<Rigidbody>();
        }

        public ControllerManager GetController()
        {
            return Controller;
        }
    }
}