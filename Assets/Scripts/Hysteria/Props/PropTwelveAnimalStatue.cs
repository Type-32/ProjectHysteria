using System;
using Hysteria.Interface;
using Hysteria.Props.Overseer;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Props
{
    public class PropTwelveAnimalStatue : PropBehaviour
    {
        public GameObject lightObject;
        public int statueId = 0;
        private bool _lightActivated = false;
        public UnityEvent<bool> onStatueInteracted;

        internal PropTwelveAnimalOverseer _overseer;

        private void Start()
        {
            lightObject.SetActive(_lightActivated);
        }

        public override void Interact()
        {
            base.Interact();
            _lightActivated = !_lightActivated;
            lightObject.SetActive(_lightActivated);

            if(_lightActivated)
            {
                _overseer.AddSequence(statueId);
                _overseer.CheckSequence();
            }
            else
                _overseer.RemoveSequence(statueId);
            
            onStatueInteracted.Invoke(_lightActivated);
        }
    }
}