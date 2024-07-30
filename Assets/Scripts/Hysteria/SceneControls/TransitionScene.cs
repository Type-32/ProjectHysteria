using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hysteria.SceneControls
{
    public class TransitionScene : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField, Scene] private string transitionScene;
        private bool _allowUpdateStatus = false;
    
        void Start()
        {
            
        }

        private void OnEnable()
        {
            _allowUpdateStatus = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_allowUpdateStatus)
                slider.value = SceneController.Instance.LoadingProgress;
            if (SceneController.Instance.LoadingProgress >= 1f) _allowUpdateStatus = false;
        }
    }
}
