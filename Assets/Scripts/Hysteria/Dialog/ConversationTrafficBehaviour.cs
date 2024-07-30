using System;
using System.Collections.Generic;
using Hysteria.Controller;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Hysteria.Dialog
{
    public class ConversationTrafficBehaviour : MonoBehaviour
    {
        [ShowNonSerializedField] private ControllerManager Controller;
        public List<ConversationObject> Conversations = new();
        [ShowNonSerializedField] private int CurrentConversation = -1;
        
        [Space]
        [SerializeField] private GameObject holder;
        [SerializeField] private RectTransform leftBG, rightBG;
        [SerializeField] private Text leftTitleElement, leftContentElement;
        [SerializeField] private Image leftSpriteElement;
        [SerializeField] private Text rightTitleElement, rightContentElement;
        [SerializeField] private Image rightSpriteElement;
        [SerializeField] private Slider timerElement;
        [SerializeField, ReadOnly] private List<Button> options;
        [SerializeField] private int selectedOptionIndex = 0;

        public Button SelectedOption
        {
            get
            {
                return options[selectedOptionIndex];
            }
        }

        private ConversationControls _map;
        private ConversationObject _currentConversationObject;

        private UnityEvent _cachedAfterEvent;

        private int _dialogDataIndex = -1;

        private static ConversationTrafficBehaviour _instance;

        public static ConversationTrafficBehaviour Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<ConversationTrafficBehaviour>();
                return _instance;
            }
        }

        public bool InConversation => CurrentConversation > -1;

        public void EnableInputs()
        {
            _map.Enable();
            _map.Primary.Continue.performed += OnPerformContinue;
            _map.Primary.SelectNext.performed += OnPerformSelectNext;
            _map.Primary.SelectPrevious.performed += OnPerformSelectPrevious;
        }

        public void DisableInputs()
        {
            _map.Disable();
            _map.Primary.Continue.performed -= OnPerformContinue;
            _map.Primary.SelectNext.performed -= OnPerformSelectNext;
            _map.Primary.SelectPrevious.performed -= OnPerformSelectPrevious;
        }

        private void Awake()
        {
            _instance = this;
            Controller = FindObjectOfType<ControllerManager>();
            _map = new ConversationControls();
            _dialogDataIndex = -1;
            
            holder.SetActive(false);
        }

        private void Update()
        {
            if (InConversation)
            {
                // Update timer if needed
                if (timerElement)
                {
                    if(timerElement.value > 0) timerElement.value -= Time.deltaTime;
                }
            }
        }

        protected void OnDialogIndexChanged(int from, int to)
        {
            if (!InConversation) return;
            
            _dialogDataIndex = to;
            UpdateDialogUI();
        }

        private void UpdateDialogUI()
        {
            if (!_currentConversationObject || _dialogDataIndex < 0 || _dialogDataIndex >= _currentConversationObject.Dialogs.Count)
                return;

            DialogData currentDialog = _currentConversationObject.Dialogs[_dialogDataIndex];

            if (currentDialog.UseRightBackground)
            {
                leftBG.gameObject.SetActive(false);
                rightBG.gameObject.SetActive(true);
                rightTitleElement.text = currentDialog.Character.CharacterName;
                rightContentElement.text = currentDialog.CharacterContent;
                rightSpriteElement.sprite = currentDialog.Character.CharacterSprite;
            }
            else
            {
                leftBG.gameObject.SetActive(true);
                rightBG.gameObject.SetActive(false);
                leftTitleElement.text = currentDialog.Character.CharacterName;
                leftContentElement.text = currentDialog.CharacterContent;
                leftSpriteElement.sprite = currentDialog.Character.CharacterSprite;
            }

            // Show/hide options based on DialogType
            bool isMultiResponse = currentDialog.DialogType == DialogType.MultiResponse;

            if (isMultiResponse)
            {
                // Update option texts if needed
                
            }
        }

        public void InvokeConversation(int index, UnityEvent afterEvent = null)
        {
            if (InConversation) return;
            CurrentConversation = index;
            Controller.InputManager.DisableInputs();
            EnableInputs();
            
            _currentConversationObject = Conversations[index];
            _dialogDataIndex = 0;
            UpdateDialogUI();

            if (afterEvent != null)
                _cachedAfterEvent = afterEvent;
            
            holder.SetActive(true);
        }

        public void ExitConversation()
        {
            CurrentConversation = -1;
            Controller.InputManager.EnableInputs();
            DisableInputs();
            
            holder.SetActive(InConversation);
        }
        
        #region Inputs

        protected void OnPerformContinue(InputAction.CallbackContext context)
        {
            ContinueConversation();
        }
        protected void OnPerformSelectNext(InputAction.CallbackContext context)
        {
            if (!InConversation) return;

            if (_dialogDataIndex < _currentConversationObject.Dialogs.Count - 1)
            {
                _dialogDataIndex++;
                UpdateDialogUI();
            }
            else
            {
                ExitConversation();
            }
            
            if(_cachedAfterEvent != null)
                _cachedAfterEvent?.Invoke();
        }
        protected void OnPerformSelectPrevious(InputAction.CallbackContext context)
        {
            
        }

        public void ContinueConversation()
        {
            if (!InConversation) return;

            if (_dialogDataIndex < _currentConversationObject.Dialogs.Count - 1)
            {
                _dialogDataIndex++;
                UpdateDialogUI();
            }
            else
            {
                ExitConversation();
            }
            
            if(_cachedAfterEvent != null)
                _cachedAfterEvent?.Invoke();
        }
        #endregion
    }
}