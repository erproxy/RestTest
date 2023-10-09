using System;
using RestTest.StateMachine;
using Models.DataModels;
using Models.WebTool;
using RestTest.Controllers.Core;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using UnityEngine;
using VContainer;
#pragma warning disable CS4014

namespace Models.Controllers
{
    public class GameSceneControl : MonoBehaviour
    {
        [SerializeField] private GameObject _battleStage;
        
        [Inject] private readonly ICoreStateMachine _coreStateMachine;
        [Inject] private readonly IWindowManager _windowManager;
        [Inject] private readonly IPlayerGameStatsRoot _playerGameStats;
        [Inject] private readonly IDataCentralService _dataCentralService;
        [Inject] private readonly ConfigManager _configManager;
        [Inject] private readonly IWebToolService _webToolService;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private LobbyModel _lobbyModel;

        private void Awake()
        {
            _lobbyModel = new LobbyModel(_windowManager, _playerGameStats, _configManager, _webToolService);
        }

        public void OnEnable()
        {
            _coreStateMachine.GameStateMachine.GameState.Subscribe(OnGameStateSwitch).AddTo(_disposables);
            _coreStateMachine.GameStateMachine.SetGameState(GameStateEnum.Lobby);
        }

        private void OnDisable()
        {
            _disposables.Clear();
        }

        private void OnGameStateSwitch(GameStateEnum gameStateEnum)
        {
            switch (gameStateEnum)
            {
                case GameStateEnum.Lobby:
                    if (string.IsNullOrEmpty(_dataCentralService.StatsDataModel.PlayerName.Value))
                    {
                        LoggingWindow loggingWindow = _windowManager.Show<LoggingWindow>();
                        loggingWindow.Logged += OnLogged;

                        void OnLogged(string name)
                        {
                            loggingWindow.Logged -= OnLogged;
                            _dataCentralService.StatsDataModel.SetPlayerName(name);
                            SetLobby();
                            _dataCentralService.SaveStatsDataModel();
                        }
                    }
                    else
                    {
                        SetLobby();
                    }
                    break;
                case GameStateEnum.StageBattle:
                    _windowManager.Hide<TopPanelWindow>();
                    _windowManager.Hide<LobbyWindow>();
                    _battleStage.SetActive(true);
                    break;
            }
        }

        private void SetLobby()
        {
            _windowManager.Show<LobbyWindow>();
            _windowManager.Show<TopPanelWindow>(WindowPriority.TopPanel);
            _lobbyModel.Activate();
            _lobbyModel.FindEnemy();
        }
    }
}