using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hysteria.Props.Overseer
{
    public class PropTwelveAnimalOverseer : MonoBehaviour
    {
        [SerializeField] protected List<PropTwelveAnimalStatue> RelatedObjects = new();
        private List<int> interactionSequence = new();
        public int maxCount = 12;

        private bool _flag = false;

        public UnityEvent onSolvePuzzle;

        protected void Awake()
        {
            foreach (var i in RelatedObjects)
                i._overseer = this;
        }

        public void AddSequence(int id)
        {
            if (interactionSequence.Count > maxCount) return;
            interactionSequence.Add(id);
        }
        
        public void RemoveSequence(int id)
        {
            interactionSequence.Remove(id);
        }

        public bool CheckSequence()
        {
            if (interactionSequence.Count < maxCount) return false;
            
            for (int i = 1; i < interactionSequence.Count; i++)
            {
                if (interactionSequence[i] < interactionSequence[i-1])
                    return false;
            }

            return true;
        }

        private void FixedUpdate()
        {
            if (!_flag)
            {
                _flag = CheckSequence();
                if (_flag)
                {
                    onSolvePuzzle?.Invoke();
                }
            }
        }
    }
}