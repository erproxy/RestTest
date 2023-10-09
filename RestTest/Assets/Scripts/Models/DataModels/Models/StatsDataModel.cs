using Models.DataModels.Data;
using UniRx;

namespace Models.DataModels.Models
{
 public interface IStatsDataModel
    {
        #region Fields

        IReadOnlyReactiveProperty<long> CoinsCount { get; }

        IReadOnlyReactiveProperty<string> PlayerName { get; }

        #endregion
        
        #region Setters
        void SetCoinsCount(long value);
        void AddCoinsCount(long value);
        void MinusCoinsCount(long value);
        void SetPlayerName(string value);

        #endregion
    }
    
    public class StatsDataModel : IStatsDataModel
    {
        #region Fields

        private ReactiveProperty<long> _coinsCount = new ReactiveProperty<long>();
        private ReactiveProperty<string> _playerName = new ReactiveProperty<string>();
        
        public IReadOnlyReactiveProperty<long> CoinsCount => _coinsCount;
        public IReadOnlyReactiveProperty<string> PlayerName => _playerName;

        #endregion

        #region Setters
        public void SetCoinsCount(long value) => _coinsCount.Value = value;
        public void AddCoinsCount(long value) => _coinsCount.Value += value;
        public void MinusCoinsCount(long value) => _coinsCount.Value -= value;
        public void SetPlayerName(string value) => _playerName.Value = value;
        
        #endregion

        #region Storage
        public StatsData GetStatsData()
        {
            
            StatsData statsData = new StatsData
            {
                CoinsCount = _coinsCount.Value,
                PlayerName = _playerName.Value,
            };
            return statsData;
        }

        public void SetStatsData(StatsData statsData)
        {
            _coinsCount.Value = statsData.CoinsCount;
            _playerName.Value = statsData.PlayerName;
        }
        
        public void SetAndInitEmptyStatsData(StatsData statsData)
        {
            SetStatsData(statsData);
        }
        #endregion  
    }
}