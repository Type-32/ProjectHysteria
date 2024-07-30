using System;
using NaughtyAttributes;
using UnityEngine;

namespace Hysteria.Dialog
{
    [Serializable]
    public class DialogData
    {
        [Dropdown("DialogCharacters")] public DialogCharacterObject Character;
        [ResizableTextArea, TextArea(100, 100)] public string CharacterContent = "";
        public DialogType DialogType = DialogType.SimpleResponse;
        public bool UseRightBackground = false;
    }
}