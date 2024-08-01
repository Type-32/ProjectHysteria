using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Props
{
    public class PropObjectTogglerBehaviour : PropBehaviour
    {
        [InfoBox("Objects that will be enabled or disabled.")]
        public List<GameObject> objects = new();

        [Title("Events", "Invoked when the methods are called.")]
        public UnityEvent onObjectStatesChanged;

        public UnityEvent onEnabledObjects, onDisabledObjects;

        public void EnableObjects()
        {
            foreach (var obj in objects)
            {
                obj.SetActive(true);
            }
            onObjectStatesChanged?.Invoke();
            onEnabledObjects?.Invoke();
        }
        
        public void DisableObjects()
        {
            foreach (var obj in objects)
            {
                obj.SetActive(true);
            }
            onObjectStatesChanged?.Invoke();
            onDisabledObjects?.Invoke();
        }
    }
}