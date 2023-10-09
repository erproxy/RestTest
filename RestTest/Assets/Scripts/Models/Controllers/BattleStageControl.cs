using System;
using Models.DataModels;
using Models.Units;
using RestTest.Controllers.Core;
using RestTest.StateMachine;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using UnityEngine;
using VContainer;
using Views;
using Random = UnityEngine.Random;

namespace Models.Controllers
{
    public class BattleStageControl : MonoBehaviour
    {
        [SerializeField] private EnemyUnitView _enemyUnitView;

        private EnemyUnitModel _enemyUnitModel;
        
        [Inject] private readonly IWindowManager _windowManager;
        [Inject] private readonly IPlayerGameStats _playerGameStats;
        [Inject] private readonly ConfigManager _configManager;
        [Inject] private readonly IDataCentralService _dataCentralService;
        [Inject] private readonly ICoreStateMachine _coreStateMachine;

        private void Awake()
        {
            _enemyUnitModel = new EnemyUnitModel(_enemyUnitView);
        }

        private void OnEnable()
        {
            _windowManager.Show<BattleWindow>().InputButton.OnClickAsObservable().TakeUntilDisable(this).Subscribe(_ => HitEnemy());
        
            _enemyUnitModel.SetHealth( Random.Range(_configManager.StatsSO.StatsTestRestConfig.EnemyHealth.MinValue,
                _configManager.StatsSO.StatsTestRestConfig.EnemyHealth.MaxValue));
            _enemyUnitModel.EnemyIsDead += EnemyDead;
        }

        private void OnDisable()
        {
            _enemyUnitModel.EnemyIsDead -= EnemyDead;
        }

        private void HitEnemy()
        {
            _enemyUnitModel.Hit(Random.Range(_configManager.StatsSO.StatsTestRestConfig.PlayerDamage.MinValue,
                _configManager.StatsSO.StatsTestRestConfig.PlayerDamage.MaxValue));
        }

        private void EnemyDead()
        {
            int reward = Random.Range(_configManager.StatsSO.StatsTestRestConfig.BattleReward.MinValue,
                _configManager.StatsSO.StatsTestRestConfig.BattleReward.MaxValue);
            var playResultWindow = _windowManager.Show<PlayResultWindow>(WindowPriority.AboveTopPanel);
            playResultWindow.SetStats(reward, _playerGameStats.EnemyModel.Value.Name);
            playResultWindow.Claim += OnClaim;

            void OnClaim()
            {
                playResultWindow.Claim -= OnClaim;
                _dataCentralService.StatsDataModel.AddCoinsCount(reward);
                _dataCentralService.SaveStatsDataModel();
                _windowManager.Hide(playResultWindow);
                _coreStateMachine.GameStateMachine.SetGameState(GameStateEnum.Lobby);
                gameObject.SetActive(false);
            }
        }
    }
}