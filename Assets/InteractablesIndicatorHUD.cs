using System;
using System.Collections;
using System.Collections.Generic;
using Hysteria.Interface;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InteractablesIndicatorHUD : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform trackerHolder;
    private Dictionary<IInteractableObject, RectTransform> trackers = new();
    [SerializeField] private GameObject trackerPrefab;

    void Start()
    {
        _camera = Camera.main;
        if (!canvasRectTransform)
        {
            canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }
        if (!trackerHolder)
        {
            Debug.LogWarning("Tracker holder not set. Using this GameObject's RectTransform.");
            trackerHolder = GetComponent<RectTransform>();
        }
    }

    void FixedUpdate()
    {
        UpdateTrackerPositions();
    }

    private void UpdateTrackerPositions()
    {
        foreach (var kvp in trackers)
        {
            if (kvp.Key.GetGameObject() != null)
            {
                Vector3 worldPosition = kvp.Key.GetGameObject().transform.position;
                Vector3 viewportPosition = _camera.WorldToViewportPoint(worldPosition);
                Vector2 screenPosition = new Vector2(
                    ((viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
                    ((viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f))
                );

                kvp.Value.anchoredPosition = screenPosition;

                // Hide the tracker if the object is behind the camera
                kvp.Value.gameObject.SetActive(viewportPosition.z > 0);
            }
        }
    }
    
    public void UpdateTrackingObject(IInteractableObject target, Color color, float scale)
    {
        if (!trackers.TryGetValue(target, out RectTransform tracker))
        {
            tracker = Instantiate(trackerPrefab, trackerHolder).GetComponent<RectTransform>();
            tracker.SetParent(trackerHolder, false);
            trackers.Add(target, tracker);
        }

        Image img = tracker.GetComponentInChildren<Image>();
        if (img != null)
        {
            img.color = color;
            tracker.localScale = Vector3.one * scale;
        }
    }

    public void RemoveTrackingObject(IInteractableObject target)
    {
        if (trackers.TryGetValue(target, out RectTransform tracker))
        {
            if (tracker)
                Destroy(tracker.gameObject);
            trackers.Remove(target);
        }
    }
}