using Hysteria.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hysteria.Props
{
    public class PropBehaviour : MonoBehaviour, IInteractableObject<PropBehaviour>
    {
        [Title("Configuration")] public bool isPropInteractable = true;
        
        private bool _interactedOneTime = false;
        
        object IInteractableObject.GetObject()
        {
            return GetObject();
        }

        public virtual PropBehaviour GetObject()
        {
            return null;
        }

        public virtual void Interact()
        {
            _interactedOneTime = true;
        }

        public bool IsInteractable()
        {
            return isPropInteractable;
        }

        public bool InteractedOneTime()
        {
            return _interactedOneTime;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}