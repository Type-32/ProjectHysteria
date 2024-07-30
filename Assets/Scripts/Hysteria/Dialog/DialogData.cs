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
        [AssetSelector(Filter = "p: t:DialogCharacterObject")] public DialogCharacterObject Character;
        [ResizableTextArea, TextArea(5, 10)] public string CharacterContent = "";
        
        [Title("Options")]
        [EnumToggleButtons]
        public DialogType DialogType = DialogType.SimpleResponse;
        public bool UseRightBackground = false;
        
        [NaughtyAttributes.ShowIf("DialogType", DialogType.MultiResponse), AssetSelector(Filter = "p: t:DialogOptionSet")]
        public DialogOptionSet options;
    }
}