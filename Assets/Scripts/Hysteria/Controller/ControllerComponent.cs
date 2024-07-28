using System;
using UnityEngine;

namespace Hysteria.Controller
{
    public class ControllerComponent : MonoBehaviour
    {
        protected ControllerManager Controller;
        protected ControllerInputManager InputManager;
        protected ControllerMovement Movement;
        protected Rigidbody RB;

        protected virtual void Awake()
        {
            GetReferences();
        }

        public ControllerManager GetController()
        {
            return Controller;
        }

        public void GetReferences()
        {
            if(Controller == null) Controller = GetComponent<ControllerManager>();
            if(InputManager == null) InputManager = GetComponent<ControllerInputManager>();
            if(Movement == null) Movement = GetComponent<ControllerMovement>();
            if(RB == null) RB = GetComponent<Rigidbody>();
        }
    }
}