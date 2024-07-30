using NaughtyAttributes;
using UnityEngine;

namespace Hysteria.Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Character", menuName = "Hysteria/Dialog Data", order = 0)]
    public class DialogCharacterObject : ScriptableObject
    {
        public string CharacterName;
        [ShowAssetPreview] public Sprite CharacterSprite;
    }
}