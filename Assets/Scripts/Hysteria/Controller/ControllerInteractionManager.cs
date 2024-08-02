using System;
using System.Collections.Generic;
using Hysteria.Interface;
using JetBrains.Annotations;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Hysteria.Controller
{
    public class ControllerInteractionManager : ControllerComponent
    {
        [Sirenix.OdinInspector.MinValue(0f), Sirenix.OdinInspector.MaxValue(5f), SerializeField]
        protected float detectionRange = 4f, interactionRange = 2f;

        [Tag, SerializeField]
        protected string lookForTag = "";
        
        [CanBeNull]
        public IInteractableObject SelectedObject
        {
            get;
            protected set;
        }

        private List<Collider> cachedColliders = new();
        private Dictionary<IInteractableObject, float> trackedObjects = new Dictionary<IInteractableObject, float>();

        [CanBeNull] private InteractablesIndicatorHUD _interactablesIndicatorHUD;

        [SerializeField] private Color detectionRangeColor = Color.gray;
        [SerializeField] private Color interactionRangeColor = Color.white;
        [SerializeField] private float detectionRangeScale = 0.8f;
        [SerializeField] private float interactionRangeScale = 1f;

        public bool DetectThroughWalls = true;

        private void Start()
        {
            _interactablesIndicatorHUD = FindObjectOfType<InteractablesIndicatorHUD>();
        }

        private void FixedUpdate()
        {
            if (Controller.IsFirstPerson()) return;
            
            IInteractableObject closestObject = null;
            float closestDistance = detectionRange;
            Dictionary<IInteractableObject, float> objectsInRange = new Dictionary<IInteractableObject, float>();

            foreach (var c in GetVisibleCollidersInSphere(transform.position, detectionRange))
            {
                float distance = Vector3.Distance(c.transform.position, transform.position);
                
                if (distance <= detectionRange && c.gameObject.CompareTag(lookForTag))
                {
                    var interactableObject = c.gameObject.GetComponent<IInteractableObject>();
                    if (interactableObject != null)
                    {
                        objectsInRange[interactableObject] = distance;
                        
                        if (distance < closestDistance)
                        {
                            closestObject = interactableObject;
                            closestDistance = distance;
                        }
                    }
                }
            }

            // Update or remove trackers based on current objects in range
            UpdateAllTrackers(objectsInRange);

            // Update the dictionary of tracked objects
            trackedObjects = objectsInRange;
            
            SelectedObject = closestObject;
        }

        private void UpdateAllTrackers(Dictionary<IInteractableObject, float> objectsInRange)
        {
            if (!_interactablesIndicatorHUD) _interactablesIndicatorHUD = FindObjectOfType<InteractablesIndicatorHUD>();
            // if (_interactablesIndicatorHUD == null) return;

            // Update trackers for objects in range
            foreach (var kvp in objectsInRange)
            {
                UpdateTracker(kvp.Key, kvp.Value);
            }

            // Remove trackers for objects that are no longer in range
            foreach (var trackedObject in new List<IInteractableObject>(trackedObjects.Keys))
            {
                if (!objectsInRange.ContainsKey(trackedObject))
                {
                    _interactablesIndicatorHUD.RemoveTrackingObject(trackedObject);
                    trackedObjects.Remove(trackedObject);
                }
            }
        }

        private void UpdateTracker(IInteractableObject interactableObject, float distance)
        {
            Color color = distance <= interactionRange ? interactionRangeColor : detectionRangeColor;
            float scale = distance <= interactionRange ? interactionRangeScale : detectionRangeScale;
            
            _interactablesIndicatorHUD?.UpdateTrackingObject(interactableObject, color, scale);
        }
        
        private List<Collider> GetVisibleCollidersInSphere(Vector3 center, float radius)
        {
            List<Collider> visibleColliders = new List<Collider>();
            Collider[] colliders = Physics.OverlapSphere(center, radius);

            if (cachedColliders.Count == colliders.Length && cachedColliders.TrueForAll(c => Array.IndexOf(colliders, c) != -1))
                return cachedColliders;

            foreach (Collider collider in colliders)
            {
                Vector3 directionToCollider = collider.bounds.center - center;
                float distanceToCollider = directionToCollider.magnitude;

                if (DetectThroughWalls && Physics.Raycast(center, directionToCollider, out RaycastHit hit, distanceToCollider + 1) && hit.collider.Equals(collider))
                {
                    visibleColliders.Add(collider);
                }
                
                if(!DetectThroughWalls){
                    visibleColliders.Add(collider);
                }
            }

            cachedColliders = visibleColliders;
            return visibleColliders;
        }

        public void InteractSelectedObject()
        {
            if (!Controller.IsFirstPerson())
            {
                if (SelectedObject != null &&
                    Vector3.Distance(SelectedObject.GetGameObject().transform.position,
                        gameObject.transform.position) <= interactionRange)
                    SelectedObject?.Interact();
            }
            else
            {
                if (Physics.Raycast(Controller._firstPersonCamera.transform.position,
                        Controller._firstPersonCamera.transform.forward, out RaycastHit hit, interactionRange))
                {
                    if (hit.collider != null && hit.collider.gameObject.CompareTag(lookForTag))
                    {
                        IInteractableObject obj = hit.collider.gameObject.GetComponent<IInteractableObject>();
                        obj?.Interact();
                    }
                }
            }
        }
    }
}