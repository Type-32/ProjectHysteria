using System.Collections;
using Hysteria.Utility;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hysteria.SceneControls
{
    public class SceneController : Singleton<SceneController>
    {
        [SerializeField, Scene] private string transitionSceneName;
        private float _loadingProgress = 0f;
        public float LoadingProgress => _loadingProgress;

        public void LoadScene(string sceneName, bool useTransition = true)
        {
            if (useTransition)
                StartCoroutine(LoadSceneRoutine(sceneName));
            else
                SceneManager.LoadScene(sceneName);
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            _loadingProgress = 0f;

            // Load the transition scene
            yield return SceneManager.LoadSceneAsync(transitionSceneName, LoadSceneMode.Additive);
            _loadingProgress = 0.1f;

            // Capture the current active scene
            Scene initialScene = SceneManager.GetActiveScene();

            // Start loading the target scene in the background
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            // Wait until the new scene is almost loaded
            while (asyncLoad.progress < 0.9f)
            {
                _loadingProgress = 0.1f + (asyncLoad.progress * 0.8f);
                yield return null;
            }

            _loadingProgress = 0.9f;

            // Wait for a frame to ensure UI updates
            yield return null;

            // Activate the new scene
            asyncLoad.allowSceneActivation = true;

            // Wait for the scene to finish loading
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Set the newly loaded scene as active
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(newScene);

            // Unload the initial scene
            yield return SceneManager.UnloadSceneAsync(initialScene);

            // Unload the transition scene
            yield return SceneManager.UnloadSceneAsync(transitionSceneName);

            _loadingProgress = 1f;
        }

        public float GetLoadingProgress()
        {
            return _loadingProgress;
        }
        
        public Scene GetCurrentScene()
        {
            return SceneManager.GetActiveScene();
        }

        public string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        public int GetCurrentSceneBuildIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}