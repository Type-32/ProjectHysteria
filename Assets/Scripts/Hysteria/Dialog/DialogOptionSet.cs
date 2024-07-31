using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Option Set", menuName = "Hysteria/Dialog Option Set")]
    public class DialogOptionSet : ScriptableObject
    {
        public List<DialogOptionData> options = new();

        [Serializable]
        public class DialogOptionData
        {
            public string optionText = "";
            public bool isSpecial = false;
        }
    }
}