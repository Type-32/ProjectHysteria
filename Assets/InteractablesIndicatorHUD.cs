using System.Collections;
using System.Collections.Generic;
using Hysteria.Interface;
using UnityEngine;
using UnityEngine.UI;

public class InteractablesIndicatorHUD : MonoBehaviour
{
    private Camera _camera;
    private RectTransform trackerHolder;
    private Dictionary<IInteractableObject, RectTransform> trackers = new();
    [SerializeField] private GameObject trackerPrefab;
    void Start()
    {
        _camera = Camera.current;
    }

    void FixedUpdate()
    {
        foreach (var obj in trackers.Keys)
        {
            trackers.TryGetValue(obj, out RectTransform value);
            if (value)
                value.position = _camera.WorldToScreenPoint(obj.GetGameObject().transform.position);
            else
                trackers.Remove(obj);
        }
    }
    
    public RectTransform AddTrackingObject(IInteractableObject target, Color color, float ratioSize = 1f)
    {
        RectTransform obj = Instantiate(trackerPrefab, trackerHolder).GetComponent<RectTransform>();
        trackers.Add(target, obj);

        Image img = obj.GetComponentInChildren<Image>();
        if (img.gameObject)
        {
            img.color = color;
            obj.localScale = new Vector3(obj.localScale.x * ratioSize, obj.localScale.y * ratioSize,
                obj.localScale.z * ratioSize);
        }
        return obj;
    }
}
