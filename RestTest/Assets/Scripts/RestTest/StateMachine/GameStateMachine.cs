using UniRx;

namespace RestTest.StateMachine
{
    public interface IGameStateMachine
    {
        GameStateEnum LastGameState { get; }
        IReadOnlyReactiveProperty<GameStateEnum> GameState { get; }
        void SetGameState(GameStateEnum gameStateEnum);
    }
    public class GameStateMachine : IGameStateMachine
    {
        private ReactiveProperty<GameStateEnum> _gameState = new ReactiveProperty<GameStateEnum>();

        public IReadOnlyReactiveProperty<GameStateEnum> GameState => _gameState;

        private GameStateEnum _lastGameState = GameStateEnum.Lobby;
        public GameStateEnum LastGameState => _lastGameState;
        
        public void SetGameState(GameStateEnum gameStateEnum)
        {
            _lastGameState = _gameState.Value;

            _gameState.Value = gameStateEnum;
        }
    }
}