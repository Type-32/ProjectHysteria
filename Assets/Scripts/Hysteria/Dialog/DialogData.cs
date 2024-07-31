using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Dialog
{
    [Serializable]
    public class DialogData : IEquatable<DialogData>
    {
        [EnumToggleButtons]
        public DialogType DialogType = DialogType.SimpleResponse;
        
        [Title("Dialog"), Sirenix.OdinInspector.HideIf("DialogType", DialogType.MultiResponse)]
        public DialogCharacterObject Character;
        [ResizableTextArea, TextArea(5, 10), Sirenix.OdinInspector.HideIf("DialogType", DialogType.MultiResponse)] public string CharacterContent = "";
        
        [Sirenix.OdinInspector.ShowIf("DialogType", DialogType.MultiResponse)]
        public DialogOptionSet options;

        public bool showOneTime = false;

        public bool Equals(DialogData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DialogType == other.DialogType && Equals(Character, other.Character) && CharacterContent == other.CharacterContent && Equals(options, other.options) && showOneTime == other.showOneTime;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DialogData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)DialogType, Character, CharacterContent, options, showOneTime);
        }
    }
}