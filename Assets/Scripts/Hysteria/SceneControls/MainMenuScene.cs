using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Hysteria.SceneControls
{
    public class MainMenuScene : MonoBehaviour
    {
        [SerializeField] private Button start;
        [SerializeField, Scene] private string nextScene;

        private void Awake()
        {
            start.onClick.AddListener(() =>
            {
                SceneController.Instance.LoadScene(nextScene);
            });
        }
    }
}
