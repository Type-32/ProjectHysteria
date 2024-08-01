using System;
using System.Collections;
using System.Collections.Generic;
using Hysteria.Interface;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hysteria.Interface;
using UnityEngine;
using UnityEngine.UI;

public class InteractablesIndicatorHUD : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private RectTransform trackerHolder;
    private Dictionary<IInteractableObject, RectTransform> trackers = new();
    [SerializeField] private GameObject trackerPrefab;

    void Start()
    {
        _camera = Camera.main;
        trackerHolder = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        UpdateTrackers();
    }

    private void UpdateTrackers()
    {
        foreach (var obj in trackers.Keys.ToList())
        {
            if (trackers.TryGetValue(obj, out RectTransform tracker))
            {
                if (tracker && obj.GetGameObject())
                {
                    tracker.position = _camera.WorldToScreenPoint(obj.GetGameObject().transform.position);
                }
                else
                {
                    RemoveTrackingObject(obj);
                }
            }
        }
    }
    
    public RectTransform AddTrackingObject(IInteractableObject target, Color color, float ratioSize = 1f)
    {
        if (trackers.ContainsKey(target))
            return trackers[target];

        RectTransform obj = Instantiate(trackerPrefab, trackerHolder).GetComponent<RectTransform>();
        trackers.Add(target, obj);

        Image img = obj.GetComponentInChildren<Image>();
        if (img != null)
        {
            img.color = color;
            obj.localScale = new Vector3(obj.localScale.x * ratioSize, obj.localScale.y * ratioSize, obj.localScale.z * ratioSize);
        }
        return obj;
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
