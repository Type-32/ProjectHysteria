using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hysteria.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    public class ButtonSoundPlayer : MonoBehaviour, IPointerClickHandler, ISelectHandler
    {
        [SerializeField, ValueDropdown("GetAllAvailableClipNames")] protected string clickSoundName = "ButtonClick", selectSoundName = "ButtonSelect";
        [SerializeField] protected float volume = 1f;

        private void Start()
        {
            // Ensure the button has a Button component
            if (!GetComponent<Button>())
            {
                Debug.LogWarning($"ButtonSoundPlayer is attached to GameObject {gameObject.name} without a Button component.");
            }
        }

        private List<string> GetAllAvailableClipNames()
        {
            return GlobalAudioSourceManager.Instance.GetAudioClipKeys();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlaySound(clickSoundName);
        }

        public void OnSelect(BaseEventData eventData)
        {
            PlaySound(selectSoundName);
        }

        private void PlaySound(string soundName)
        {
            GlobalAudioSourceManager.Instance.PlaySound(soundName, volume);
        }
    }
}