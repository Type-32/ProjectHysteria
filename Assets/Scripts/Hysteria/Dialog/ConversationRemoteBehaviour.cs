using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Dialog
{
    public class ConversationRemoteBehaviour : MonoBehaviour
    {
        [Dropdown("GetConversationValues"), SerializeField] protected int selectedConversation;
        [SerializeField] protected bool triggersOneTime = true;
        
        [Space(10)]
        [SerializeField] private UnityEvent onConversationTriggered, afterConversationTriggered;

        private bool _triggered = false;
        
        private DropdownList<int> GetConversationValues()
        {
            DropdownList<int> temp = new();
            int index = 0;

            foreach (ConversationObject c in ConversationTrafficBehaviour.Instance.Conversations)
            {
                temp.Add(c.name, index);
                index++;
            }

            return temp;
        }
        public void TriggerConv()
        {
            ConversationTrafficBehaviour.Instance.InvokeConversation(selectedConversation, afterConversationTriggered);
            onConversationTriggered?.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_triggered) return;
            TriggerConv();
            if (triggersOneTime)
                _triggered = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered) return;
            TriggerConv();
            if (triggersOneTime)
                _triggered = true;
        }
    }
}