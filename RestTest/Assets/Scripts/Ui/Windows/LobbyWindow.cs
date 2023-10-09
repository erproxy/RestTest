using Models.Controllers;
using Models.DataModels;
using Models.Requests.Models;
using RestTest.StateMachine;
using TMPro;
using Tools.UiManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Ui.Windows
{
    public class LobbyWindow : Window
    {
        [Header("Player")]
        [SerializeField] private TMP_Text _playerNameLabel;

        [Header("Enemy")]
        [SerializeField] private TMP_Text _enemyNameLabel;
        [SerializeField] private GameObject _defaultEnemyBlockIcon;
        [SerializeField] private Image _enemyIcon;

        [Header("Lobby")]
        [SerializeField] private Button _findButton;
        [SerializeField] private Button _battleButton;

        [Inject] private readonly IPlayerGameStats _playerGameStats;
        [Inject] private readonly IDataCentralService _dataCentralService;
        [Inject] private readonly ICoreStateMachine _coreStateMachine;

        public Button FindButton => _findButton;

        protected override void OnActivate()
        {
            base.OnActivate();
            _playerNameLabel.text = _dataCentralService.StatsDataModel.PlayerName.Value;
            _playerGameStats.EnemyModel.Subscribe(SetEnemyState).AddTo(ActivateDisposables);
            _battleButton.OnClickAsObservable().Subscribe(_ => OnBattle()).AddTo(ActivateDisposables);
        }

        private void SetEnemyState(EnemyRequest enemyRequest)
        {
            if (string.IsNullOrEmpty(enemyRequest.Name))
            {
                _defaultEnemyBlockIcon.SetActive(true);
                _enemyIcon.gameObject.SetActive(false);
                _enemyIcon.sprite = enemyRequest.Icon;
                _battleButton.interactable = false;
                _enemyNameLabel.text = "Поиск...";
            }
            else
            {
                _defaultEnemyBlockIcon.SetActive(false);
                _enemyIcon.gameObject.SetActive(true);
                _enemyIcon.sprite = enemyRequest.Icon;
                _battleButton.interactable = true;
                _enemyNameLabel.text = enemyRequest.Name;
            } 
        }

        private void OnBattle()
        {
            _coreStateMachine.GameStateMachine.SetGameState(GameStateEnum.StageBattle);
        }
    }
}