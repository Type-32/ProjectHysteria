//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Hysteria/Controller/ControllerInputMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Hysteria.Controller
{
    public partial class @ControllerInputMap: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @ControllerInputMap()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControllerInputMap"",
    ""maps"": [
        {
            ""name"": ""Topdown"",
            ""id"": ""a3cd51b5-bc3f-454b-91d5-bb2af3a87c0d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""032a1566-e06a-44f6-9729-929461f8f113"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""16c021c8-c134-4699-97e4-edb1204af6af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LMB"",
                    ""type"": ""Button"",
                    ""id"": ""abc83952-f387-4996-af55-2a3cdc1bbcb6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""6e5e3d4d-e660-4d47-a249-ea6286a382c9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""30defba0-2da7-4490-877b-677d75671561"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2409bdc8-212a-4b2f-a4d4-fe6a8f88d19f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2e2374db-42e4-4e4d-83ba-da80c49ada5a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a35b4f99-8822-4f73-a674-8dac782a9089"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""9033fb30-e68d-497d-ae32-8f7876601044"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cfb620ee-90e9-4771-9388-c720772eb3db"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c83cb89-030d-4ad1-bdee-e0b9ecaab90a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4994b51d-efc5-4386-8963-ef1beccb6e84"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b499bf1-5351-4a6c-95bd-3f32d398f12f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""91dd5ddd-ff81-4621-88a3-4a5428709aea"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8561aed3-fb27-4c47-89b3-e89572a1a475"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""717978b6-4d0d-4bb8-a018-a37af05ec186"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""FP"",
            ""id"": ""3c258389-0a3c-4e56-afe0-a198aaea4f41"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""033ac9c8-dee2-4dc9-8525-19019dd5b055"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""4acc914d-7dc3-4468-a3f3-e151d9d1dd61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""448a83bc-5213-47d7-a1da-2378f6fc91ff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""8d824139-8d4b-45d3-adc5-6038c335b740"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6b1c8e27-0385-4ad1-86dc-58e603813ea0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b58a04b3-32f9-4e1c-9d08-0eb1835afc20"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""24bef707-c464-4cfc-94c7-16b03f95eeee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f0dfd128-9b35-4d0f-90a5-cded398e51ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""a91af5f5-1b41-4fa2-8d11-9130fef129eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0263e2db-91bb-44a6-8952-f19646d08981"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8054e1f1-f5eb-4040-86f5-f459e92724c1"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e4d51ae-ed7d-4ee9-a6fa-2349bc870718"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5a7cfc29-5e60-46d6-a788-6d2efbcf9f8b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7ce2afc3-538e-4018-bb71-aca2cf40f48f"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d494b3a5-1c4c-462e-b71f-67a9a3615dba"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f59bd22-478e-4fec-9e63-9361d1a30387"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecbc43ef-1448-4923-a275-b6111549e30d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Topdown
            m_Topdown = asset.FindActionMap("Topdown", throwIfNotFound: true);
            m_Topdown_Movement = m_Topdown.FindAction("Movement", throwIfNotFound: true);
            m_Topdown_Interact = m_Topdown.FindAction("Interact", throwIfNotFound: true);
            m_Topdown_LMB = m_Topdown.FindAction("LMB", throwIfNotFound: true);
            // FP
            m_FP = asset.FindActionMap("FP", throwIfNotFound: true);
            m_FP_Movement = m_FP.FindAction("Movement", throwIfNotFound: true);
            m_FP_Interact = m_FP.FindAction("Interact", throwIfNotFound: true);
            m_FP_CameraLook = m_FP.FindAction("CameraLook", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Topdown
        private readonly InputActionMap m_Topdown;
        private List<ITopdownActions> m_TopdownActionsCallbackInterfaces = new List<ITopdownActions>();
        private readonly InputAction m_Topdown_Movement;
        private readonly InputAction m_Topdown_Interact;
        private readonly InputAction m_Topdown_LMB;
        public struct TopdownActions
        {
            private @ControllerInputMap m_Wrapper;
            public TopdownActions(@ControllerInputMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Topdown_Movement;
            public InputAction @Interact => m_Wrapper.m_Topdown_Interact;
            public InputAction @LMB => m_Wrapper.m_Topdown_LMB;
            public InputActionMap Get() { return m_Wrapper.m_Topdown; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(TopdownActions set) { return set.Get(); }
            public void AddCallbacks(ITopdownActions instance)
            {
                if (instance == null || m_Wrapper.m_TopdownActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_TopdownActionsCallbackInterfaces.Add(instance);
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @LMB.started += instance.OnLMB;
                @LMB.performed += instance.OnLMB;
                @LMB.canceled += instance.OnLMB;
            }

            private void UnregisterCallbacks(ITopdownActions instance)
            {
                @Movement.started -= instance.OnMovement;
                @Movement.performed -= instance.OnMovement;
                @Movement.canceled -= instance.OnMovement;
                @Interact.started -= instance.OnInteract;
                @Interact.performed -= instance.OnInteract;
                @Interact.canceled -= instance.OnInteract;
                @LMB.started -= instance.OnLMB;
                @LMB.performed -= instance.OnLMB;
                @LMB.canceled -= instance.OnLMB;
            }

            public void RemoveCallbacks(ITopdownActions instance)
            {
                if (m_Wrapper.m_TopdownActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ITopdownActions instance)
            {
                foreach (var item in m_Wrapper.m_TopdownActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_TopdownActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public TopdownActions @Topdown => new TopdownActions(this);

        // FP
        private readonly InputActionMap m_FP;
        private List<IFPActions> m_FPActionsCallbackInterfaces = new List<IFPActions>();
        private readonly InputAction m_FP_Movement;
        private readonly InputAction m_FP_Interact;
        private readonly InputAction m_FP_CameraLook;
        public struct FPActions
        {
            private @ControllerInputMap m_Wrapper;
            public FPActions(@ControllerInputMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_FP_Movement;
            public InputAction @Interact => m_Wrapper.m_FP_Interact;
            public InputAction @CameraLook => m_Wrapper.m_FP_CameraLook;
            public InputActionMap Get() { return m_Wrapper.m_FP; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(FPActions set) { return set.Get(); }
            public void AddCallbacks(IFPActions instance)
            {
                if (instance == null || m_Wrapper.m_FPActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_FPActionsCallbackInterfaces.Add(instance);
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @CameraLook.started += instance.OnCameraLook;
                @CameraLook.performed += instance.OnCameraLook;
                @CameraLook.canceled += instance.OnCameraLook;
            }

            private void UnregisterCallbacks(IFPActions instance)
            {
                @Movement.started -= instance.OnMovement;
                @Movement.performed -= instance.OnMovement;
                @Movement.canceled -= instance.OnMovement;
                @Interact.started -= instance.OnInteract;
                @Interact.performed -= instance.OnInteract;
                @Interact.canceled -= instance.OnInteract;
                @CameraLook.started -= instance.OnCameraLook;
                @CameraLook.performed -= instance.OnCameraLook;
                @CameraLook.canceled -= instance.OnCameraLook;
            }

            public void RemoveCallbacks(IFPActions instance)
            {
                if (m_Wrapper.m_FPActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IFPActions instance)
            {
                foreach (var item in m_Wrapper.m_FPActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_FPActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public FPActions @FP => new FPActions(this);
        public interface ITopdownActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnLMB(InputAction.CallbackContext context);
        }
        public interface IFPActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnCameraLook(InputAction.CallbackContext context);
        }
    }
}
