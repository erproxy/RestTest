using System.Linq;
using Cysharp.Threading.Tasks;
using Models.Requests.Models;
using Models.WebTool;
using RestTest.Controllers.Core;
using Tools.Extensions;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using UnityEngine;

namespace Models.Controllers
{
    public class LobbyModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IPlayerGameStats _playerGameStats;
        private readonly ConfigManager _configManager;
        private readonly IWebToolService _webToolService;
        private LobbyWindow _lobbyWindow;
        private FindingWindow _findingWindow;

        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public LobbyModel(IWindowManager windowManager, IPlayerGameStats playerGameStats, ConfigManager configManager, IWebToolService webToolService)
        {
            _windowManager = windowManager;
            _configManager = configManager;
            _webToolService = webToolService;
            _playerGameStats = playerGameStats;
            _lobbyWindow = _windowManager.GetWindow<LobbyWindow>();
        }
        
        public void Activate()
        {
#pragma warning disable CS4014
            _lobbyWindow.FindButton.OnClickAsObservable().Subscribe(_ => FindEnemy()).AddTo(_disposable);
#pragma warning restore CS4014
            _lobbyWindow.IsShowingReactive.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                if (!value)
                {
                    Deactivate();
                }
            }).AddTo(_disposable);
        }

        private void Deactivate()
        {
            _disposable.Clear();
        }

        public async UniTaskVoid FindEnemy()
        {
            _playerGameStats.SetEnemyRequest(default);
            _windowManager.Show<FindingWindow>();
            string json = await _webToolService.LoadJson(_configManager.StatsSO.StatsTestRestConfig.EnemyURL);
            EnemyRequestData enemyRequestData = JsonUtility.FromJson<EnemyRequestData>(json);
            
            EnemyResults enemyResults = enemyRequestData.results.FirstOrDefault();
            
            EnemyRequest enemyRequest = new EnemyRequest
            {
                Name = enemyResults.name.first
            };
            
            Texture2D texture2D = await _webToolService.LoadTexture2D(enemyResults.picture.large);
            
            if (texture2D != null)
            {
                enemyRequest.Icon = texture2D.CreateSprite();
            }
            else
            {
                enemyRequest.Icon = default;
            }
            
            Observable.Timer(System.TimeSpan.FromMilliseconds(500))
                .Subscribe(_ =>
                {
                    _playerGameStats.SetEnemyRequest(enemyRequest);
                    _windowManager.Hide<FindingWindow>();
                });
        }
    }
}