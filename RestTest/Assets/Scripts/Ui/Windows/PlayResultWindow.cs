using System;
using TMPro;
using Tools.UiManager;
using Ui.Content.PlayResult;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class PlayResultWindow : Window
    {
        [Header("WinBlock")]
        [SerializeField] private TMP_Text _title;
        [SerializeField] private ResultRewardBlock _coinRewardCountBlock;
        [SerializeField] private Button _claimButton;

        public event Action Claim;
        protected override void OnActivate()
        {
            base.OnActivate();
            _claimButton.OnClickAsObservable().Subscribe(_ => OnClaim()).AddTo(ActivateDisposables);
        }

        public void SetStats(int coinCount, string enemyName)
        {
            _title.text = $"Бой с {enemyName}";

            _coinRewardCountBlock.SetValue(coinCount);
        }

        private void OnClaim()
        {
            Claim?.Invoke();
        }
    }
}