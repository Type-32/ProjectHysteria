using System;
using System.Collections.Generic;
using Hysteria.Controller;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Hysteria.Dialog
{
    public class ConversationTrafficBehaviour : MonoBehaviour
    {
        public delegate void SelectedOptionCallback(DialogOptionSet set, int selectedIndex);
        
        [ShowNonSerializedField] private ControllerManager Controller;
        public List<ConversationObject> Conversations = new();
        [ShowNonSerializedField] private int CurrentConversation = -1;
        
        [Title("Dialog References")]
        [SerializeField] private GameObject holder;
        [SerializeField] private RectTransform dialogBG;
        [SerializeField] private Text titleElement;
        [SerializeField] private Text contentElement;
        [SerializeField] private Image spriteElement;
        [SerializeField] private RectTransform optionsHolder;

        [Title("References")] [SerializeField] private GameObject buttonPrefab;

        private ConversationControls _map;
        private ConversationObject _currentConversationObject;
        private List<Button> _options = new();

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

        public static event SelectedOptionCallback OnSelectOption;

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
            optionsHolder.gameObject.SetActive(false);
        }

        // private void Update()
        // {
        //     if (InConversation)
        //     {
        //         // Update timer if needed
        //         // if (timerElement)
        //         // {
        //         //     if(timerElement.value > 0) timerElement.value -= Time.deltaTime;
        //         // }
        //     }
        // }

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
            bool isMultiResponse = currentDialog.DialogType == DialogType.MultiResponse;
            optionsHolder.gameObject.SetActive(false);
            
            if (isMultiResponse)
            {
                optionsHolder.gameObject.SetActive(true);
                DialogOptionSet optionSet = currentDialog.options;
                foreach (var i in _options)
                    Destroy(i.gameObject);
                _options.Clear();

                int index = 0;
                foreach (var opt in optionSet.options)
                {
                    Button button = Instantiate(buttonPrefab, optionsHolder).GetComponent<Button>();
                    button.onClick.AddListener(() =>
                    {
                        OnSelectOption?.Invoke(optionSet, index++);
                        ContinueConversation();
                    });
                    Text text = button.GetComponentInChildren<Text>();
                    text.text = opt.optionText;

                    if (opt.isSpecial)
                    {
                        button.image.color = new Color(1, 0, 0, 0);
                    }
                    
                    _options.Add(button);
                }
            }
            else
            {
                optionsHolder.gameObject.SetActive(true);
                titleElement.text = currentDialog.Character.CharacterName;
                contentElement.text = currentDialog.CharacterContent;
                spriteElement.sprite = currentDialog.Character.CharacterSprite;
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
        
        public void InvokeConversation(ConversationObject obj, UnityEvent afterEvent = null)
        {
            if (!Conversations.Contains(obj))
                Conversations.Add(obj);
            
            InvokeConversation(Conversations.IndexOf(obj), afterEvent);
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