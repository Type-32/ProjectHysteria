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
        [SerializeField] private Button opt1, opt2, opt3, opt4;

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

        public void EnableConvInput()
        {
            _map.Enable();
            _map.Primary.Next.performed += PerformedNext;
            _map.Primary.Opt1.performed += PerformedOpt1;
            _map.Primary.Opt2.performed += PerformedOpt2;
            _map.Primary.Opt3.performed += PerformedOpt3;
            _map.Primary.Opt4.performed += PerformedOpt4;
        }

        public void DisableConvInput()
        {
            _map.Disable();
            _map.Primary.Next.performed -= PerformedNext;
            _map.Primary.Opt1.performed -= PerformedOpt1;
            _map.Primary.Opt2.performed -= PerformedOpt2;
            _map.Primary.Opt3.performed -= PerformedOpt3;
            _map.Primary.Opt4.performed -= PerformedOpt4;
        }

        private void Awake()
        {
            _instance = this;
            Controller = FindObjectOfType<ControllerManager>();
            _map = new ConversationControls();
            _dialogDataIndex = -1;
            
            holder.SetActive(false);
            
            // opt1.onClick.AddListener(() =>
            // {
            //     Opt1();
            // });
            // opt2.onClick.AddListener(() =>
            // {
            //     Opt2();
            // });
            // opt3.onClick.AddListener(() =>
            // {
            //     Opt3();
            // });
            // opt4.onClick.AddListener(() =>
            // {
            //     Opt4();
            // });

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
            opt1?.gameObject.SetActive(isMultiResponse);
            opt2?.gameObject.SetActive(isMultiResponse);
            opt3?.gameObject.SetActive(isMultiResponse);
            opt4?.gameObject.SetActive(isMultiResponse);

            if (isMultiResponse)
            {
                // Update option texts if needed
                // opt1.GetComponentInChildren<Text>().text = currentDialog.ResponseOptions[0];
                // opt2.GetComponentInChildren<Text>().text = currentDialog.ResponseOptions[1];
                // opt3.GetComponentInChildren<Text>().text = currentDialog.ResponseOptions[2];
                // opt4.GetComponentInChildren<Text>().text = currentDialog.ResponseOptions[3];
            }
        }

        public void InvokeConversation(int index, UnityEvent afterEvent = null)
        {
            if (InConversation) return;
            CurrentConversation = index;
            Controller.InputManager.DisableInputs();
            EnableConvInput();
            
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
            DisableConvInput();
            
            holder.SetActive(InConversation);
        }
        
        #region Inputs

        protected void PerformedNext(InputAction.CallbackContext context)
        {
            Next();
        }
        protected void PerformedOpt1(InputAction.CallbackContext context)
        {
            Opt1();
        }
        protected void PerformedOpt2(InputAction.CallbackContext context)
        {
            Opt2();
        }
        protected void PerformedOpt3(InputAction.CallbackContext context)
        {
            Opt3();
        }
        protected void PerformedOpt4(InputAction.CallbackContext context)
        {
            Opt4();
        }

        public void Next()
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

        public void Opt1()
        {
            
        }

        public void Opt2()
        {
            
        }

        public void Opt3()
        {
            
        }

        public void Opt4()
        {
            
        }
        #endregion
    }
}