using System;
using System.Collections.Generic;
using Hysteria.Interface;
using JetBrains.Annotations;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hysteria.Controller
{
    public class ControllerInteractionManager : ControllerComponent
    {
        [Sirenix.OdinInspector.MinValue(0f), Sirenix.OdinInspector.MaxValue(5f)]
        public float interactionRange = 1f;

        [Tag]
        public string lookForTag = "";
        
        [CanBeNull]
        public IInteractableObject SelectedObject
        {
            get;
            protected set;
        }

        private List<Collider> cachedColliders = new();
        
        private void FixedUpdate()
        {
            IInteractableObject closestObject = null;
            float distance = interactionRange + 1;
            foreach (var c in GetVisibleCollidersInSphere(transform.position, interactionRange))
            {
                float temp = Vector3.Distance(c.transform.position, transform.position);
                
                if (temp > distance) continue;
                
                if (c.gameObject.CompareTag(lookForTag))
                {
                    closestObject = c.gameObject.GetComponent<IInteractableObject>();
                }
            }

            SelectedObject = closestObject;
        }
        
        private List<Collider> GetVisibleCollidersInSphere(Vector3 center, float radius)
        {
            List<Collider> visibleColliders = new List<Collider>();
            Collider[] colliders = Physics.OverlapSphere(center, radius);

            if (cachedColliders.Equals(colliders))
                return cachedColliders;

            foreach (Collider collider in colliders)
            {
                Vector3 directionToCollider = collider.bounds.center - center;
                float distanceToCollider = directionToCollider.magnitude;

                RaycastHit hit;
                if (Physics.Raycast(center, directionToCollider, out hit, distanceToCollider + 1))
                {
                    if (hit.collider.Equals(collider))
                        visibleColliders.Add(collider);
                }
            }

            cachedColliders = visibleColliders;
            return visibleColliders;
        }
    }
}