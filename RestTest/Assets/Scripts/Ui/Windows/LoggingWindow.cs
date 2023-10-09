using System;
using TMPro;
using Tools.UiManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class LoggingWindow : Window
    {
        [SerializeField] private TMP_InputField _inputName;
        [SerializeField] private Button _continueButton;

        public event Action<string> Logged; 

        protected override void OnActivate()
        {
            base.OnActivate();
            SetActiveContinue(false);
            _inputName.onValueChanged.AsObservable().Subscribe(_ => SetActiveContinue(_inputName.text.Length > 0)).AddTo(ActivateDisposables);
            _continueButton.OnClickAsObservable().Subscribe(_ => OnNext());
        }

        private void SetActiveContinue(bool value)
        {
            _continueButton.interactable = value;
        }

        private void OnNext()
        {
            Logged?.Invoke(_inputName.text);
            _manager.Hide(this);
        }
    }
}