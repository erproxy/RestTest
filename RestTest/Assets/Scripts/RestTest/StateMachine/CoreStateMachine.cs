using System;
using UniRx;

namespace RestTest.StateMachine
{
    
    public interface ICoreStateMachine
    {
        void SetRunTimeState(RunTimeStateEnum runTimeStateEnum);
        void SetScenesState(ScenesStateEnum scenesStateEnum);
        void OnSceneEndLoadFade();
        void OnSceneEndLoad();
        IReadOnlyReactiveProperty<RunTimeStateEnum> RunTimeState { get; }

        IReadOnlyReactiveProperty<ScenesStateEnum> ScenesState { get; }
        RunTimeStateEnum LastRunTimeState { get; }
        event Action<ScenesStateEnum> SceneEndLoadFade;
        
        event Action<ScenesStateEnum> SceneEndLoad;

        #region SubStates

        IGameStateMachine GameStateMachine { get; }

        #endregion
    }

    public class CoreStateMachine : ICoreStateMachine
    {
        #region Enums

        private ReactiveProperty<RunTimeStateEnum> _runTimeState = new ReactiveProperty<RunTimeStateEnum>(RunTimeStateEnum.Pause);
        
        private ReactiveProperty<ScenesStateEnum> _scenesState = new ReactiveProperty<ScenesStateEnum>(ScenesStateEnum.Base);
        
        public IReadOnlyReactiveProperty<RunTimeStateEnum> RunTimeState => _runTimeState;
        public IReadOnlyReactiveProperty<ScenesStateEnum> ScenesState => _scenesState;
        public RunTimeStateEnum LastRunTimeState { get; private set; }
        
        public event Action<ScenesStateEnum> SceneEndLoadFade;
        
        public event Action<ScenesStateEnum> SceneEndLoad;

        #endregion
        
        #region SubStates

        private GameStateMachine _gameStateMachine = new GameStateMachine();

        public IGameStateMachine GameStateMachine => _gameStateMachine;
        #endregion

        #region SetStates

        public void SetRunTimeState(RunTimeStateEnum runTimeStateEnum)
        {
            LastRunTimeState = _runTimeState.Value;
            _runTimeState.Value = runTimeStateEnum;
        }
        
        public void SetScenesState(ScenesStateEnum scenesStateEnum)
        {
            _scenesState.Value = scenesStateEnum;
        }
        
        public void OnSceneEndLoadFade()
        {
            SceneEndLoadFade?.Invoke(_scenesState.Value);
        }
        
        public void OnSceneEndLoad()
        {
            SceneEndLoad?.Invoke(_scenesState.Value);
        }
        #endregion
    }
    
    #region Enums
    public enum  RunTimeStateEnum{Play = 0, Pause = 1}

    public enum ScenesStateEnum{Base = 0, Game = 1}

    public enum GameStateEnum{None, Lobby, StageBattle}
    #endregion
}