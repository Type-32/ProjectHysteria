using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;

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
    public class LevelScene : MonoBehaviour
    {
        [SerializeField, Scene] private string nextScene;
        
        public void NextScene()
        {
            SceneController.Instance.LoadScene(nextScene);
        }
        
        [CanBeNull]
        public static LevelScene GetLevelScene()
        {
            return FindObjectOfType<LevelScene>();
        }
    }
}