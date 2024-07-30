using System.Collections.Generic;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Conversation", menuName = "Hysteria/Conversation", order = 0)]
    public class ConversationObject : ScriptableObject
    {
        public List<DialogCharacterObject> DialogCharacters = new();
        public List<DialogData> Dialogs = new List<DialogData>();
    }
}