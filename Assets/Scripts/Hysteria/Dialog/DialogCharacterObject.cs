using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Character", menuName = "Hysteria/Dialog Character", order = 0)]
    public class DialogCharacterObject : ScriptableObject
    {
        public string CharacterName;
        [PreviewField] public Sprite CharacterSprite;
    }
}