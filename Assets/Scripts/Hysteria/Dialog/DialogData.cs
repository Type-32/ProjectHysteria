using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hysteria.Dialog
{
    [Serializable]
    public class DialogData
    {
        [Title("Dialog")]
        public DialogCharacterObject Character;
        [ResizableTextArea, TextArea(5, 10)] public string CharacterContent = "";
        
        [Title("Options")]
        [EnumToggleButtons]
        public DialogType DialogType = DialogType.SimpleResponse;
        
        [Sirenix.OdinInspector.ShowIf("DialogType", DialogType.MultiResponse)]
        public DialogOptionSet options;
    }
}