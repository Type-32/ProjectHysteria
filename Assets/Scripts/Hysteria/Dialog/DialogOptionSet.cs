using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Option Set", menuName = "Hysteria/Dialog Option Set")]
    public class DialogOptionSet : ScriptableObject
    {
        [DetailedInfoBox("Instructions",
            "'Option Text' is the text that will show up in the options menu.\n" +
            "'Is Special' will make the option red.\n" +
            "'Response Type' will determine the dialog behaviour after selecting the option.")]
        public List<DialogOptionData> options = new();

        [Serializable]
        public class DialogOptionData
        {
            public string optionText = "";
            public bool isSpecial = false;
            [EnumToggleButtons]
            public DialogOptionResponseType responseType = DialogOptionResponseType.ContinueDialog;
        }
    }
}