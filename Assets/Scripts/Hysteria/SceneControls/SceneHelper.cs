using System.Collections.Generic;
using Hysteria.MenuControls;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hysteria.SceneControls
{
    /// <summary>
    /// For managing the scene events in every level scene
    /// </summary>
    /// <notes>
    /// I added it here because at some point something will need to load the next level scene through the
    /// <see cref="SceneController"/>, so this is a utility wrapper for loading the next scene but also for managing the events in general
    /// </notes>
    /// <remarks>Make sure only one is present in a level scene</remarks>
    public class SceneHelper : MonoBehaviour
    {
        [SerializeField, Scene] private List<string> nextScene = new List<string>();
        
        public void NextScene(int index)
        {
            SceneController.Instance.LoadScene(nextScene[index]);
        }

        public void ReloadCurrentScene()
        {
            SceneController.Instance.LoadScene(SceneController.Instance.GetCurrentSceneName());
        }

        public void ExitProgram()
        {
            Application.Quit(0);
        }

        public void Pause()
        {
            PauseBehaviour.Instance.PauseResume();
        }
    }
}