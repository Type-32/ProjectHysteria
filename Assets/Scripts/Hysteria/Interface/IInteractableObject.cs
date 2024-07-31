using UnityEngine;

namespace Hysteria.Interface
{
    public interface IInteractableObject
    {
        public object GetObject();
        public void Interact();
        public bool IsInteractable();
        public bool InteractedOneTime();
        public GameObject GetGameObject();
    }

    public interface IInteractableObject<T> : IInteractableObject
    {
        public new T GetObject();
    }
}