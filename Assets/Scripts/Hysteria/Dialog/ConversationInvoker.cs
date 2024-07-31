using System;
using Hysteria.Interface;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Dialog
{
    public class ConversationInvoker : MonoBehaviour, IInteractableObject<ConversationInvoker>
    {
        [Title("Attributes")]
        [SerializeField] protected bool useCached = true;
        [SerializeField] protected bool useCollision = true;
        [SerializeField] protected bool useDialogObject = false;
        [SerializeField] protected bool isInteractable = true;
        
        [Title("Configuration")]
        [ValueDropdown("GetConversationValues"), SerializeField] protected int selectedConversation;

        [SerializeField] protected ConversationObject selectedConversationObject;
        [SerializeField] protected bool triggersOneTime = true;
        
        [Title("Events", "For other purposes, i.e. monitoring what happened after continuing the dialog")]
        [SerializeField] private UnityEvent onConversationTriggered, afterConversationTriggered;

        private bool _triggeredLock = false;
        
        private ValueDropdownList<int> GetConversationValues()
        {
            ValueDropdownList<int> temp = new();
            int index = 0;

            foreach (ConversationObject c in ConversationTrafficBehaviour.Instance.Conversations)
            {
                temp.Add(c.name, index);
                index++;
            }

            return temp;
        }
        public void TriggerConversation()
        {
            if (useDialogObject) ConversationTrafficBehaviour.Instance.InvokeConversation(selectedConversationObject, afterConversationTriggered);
            else ConversationTrafficBehaviour.Instance.InvokeConversation(selectedConversation, afterConversationTriggered);
            onConversationTriggered?.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!useCollision) return;
            
            if (_triggeredLock && triggersOneTime) return;
            TriggerConversation();
            _triggeredLock = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!useCollision) return;
            
            if (_triggeredLock && triggersOneTime) return;
            TriggerConversation();
            _triggeredLock = true;
        }

        public ConversationInvoker GetObject()
        {
            return this;
        }

        object IInteractableObject.GetObject()
        {
            return GetObject();
        }

        public void Interact()
        {
            TriggerConversation();
        }

        public bool IsInteractable()
        {
            return isInteractable;
        }

        public bool InteractedOneTime()
        {
            return _triggeredLock;
        }
    }
}