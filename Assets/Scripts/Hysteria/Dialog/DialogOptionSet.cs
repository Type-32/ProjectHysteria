using System.Collections.Generic;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Option Set", menuName = "Hysteria/Dialog Option Set")]
    public class DialogOptionSet : ScriptableObject
    {
        public List<string> options = new List<string>();
    }
}