using Models.Controllers;
using Models.DataModels;
using RestTest.StateMachine;
using UnityEngine;
using VContainer;

namespace RestTest.Controllers.Core
{
    public class GameInstance : MonoBehaviour
    {
        [Inject] private readonly ICoreStateMachine _coreStateMachine;
        [Inject] private readonly DataCentralService _dataCentralService;
        [Inject] private readonly IPlayerGameStatsRoot _playerGameStats;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            SetupFrameTimes();
            
            RegServices();
        }

        private void SetupFrameTimes()
        {
            Application.targetFrameRate = 90;
        }

        private async void RegServices()
        {
             await _dataCentralService.LoadData();
             
            _playerGameStats.Init();
            _coreStateMachine.SetScenesState(ScenesStateEnum.Game);
        }
    }
}