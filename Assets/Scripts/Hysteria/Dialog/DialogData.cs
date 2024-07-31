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
        [EnumToggleButtons]
        public DialogType DialogType = DialogType.SimpleResponse;
        
        [Title("Dialog"), Sirenix.OdinInspector.HideIf("DialogType", DialogType.MultiResponse)]
        public DialogCharacterObject Character;
        [ResizableTextArea, TextArea(5, 10), Sirenix.OdinInspector.HideIf("DialogType", DialogType.MultiResponse)] public string CharacterContent = "";
        
        [Sirenix.OdinInspector.ShowIf("DialogType", DialogType.MultiResponse)]
        public DialogOptionSet options;
    }
}