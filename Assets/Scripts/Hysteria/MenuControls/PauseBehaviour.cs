using System;
using System.Collections.Generic;
using Hysteria.SceneControls;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Hysteria.MenuControls
{
    public class PauseBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas UI;
        [SerializeField, Scene] private List<int> bannedScenes = new List<int>();
        private MenuControlMap ControlMap;
        
        public static PauseBehaviour _instance;

        public static PauseBehaviour Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<PauseBehaviour>();

                return _instance;
            }
        }

        public bool Paused { get; set; } = false;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            UI = GetComponentInChildren<Canvas>();
            ControlMap = new MenuControlMap();
        }

        private void OnEnable()
        {
            ControlMap.Enable();
            ControlMap.Menu.Pause.performed += ListenPausePerformed;
        }

        private void OnDisable()
        {
            ControlMap.Disable();
            ControlMap.Menu.Pause.performed -= ListenPausePerformed;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(bannedScenes.Contains(scene.buildIndex))
            {
                Instance.Paused = false;
                RefreshState();
            }
        }

        public void PauseResume()
        {
            if (bannedScenes.Contains(SceneController.Instance.GetCurrentSceneBuildIndex()))
                return;
            Paused = !Paused;
            RefreshState();
        }

        public static void PauseResumeStatic()
        {
            Instance.PauseResume();
        }

        public void RefreshState()
        {
            if (Paused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            
            if(!UI) UI = GetComponentInChildren<Canvas>();
            UI.gameObject.SetActive(Paused);
        }

        public void ListenPausePerformed(InputAction.CallbackContext context)
        {
            PauseResume();
        }
    }
}