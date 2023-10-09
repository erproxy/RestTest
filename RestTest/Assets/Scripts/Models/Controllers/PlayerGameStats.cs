using RestTest.Controllers.Core;
using Models.DataModels;
using Models.Requests.Models;
using UniRx;
namespace Models.Controllers
{
    public interface IPlayerGameStats
    {
        IReadOnlyReactiveProperty<EnemyRequest> EnemyModel { get; }

        void SetEnemyRequest(EnemyRequest enemyRequest);
    }

    public interface IPlayerGameStatsRoot : IPlayerGameStats
    {
        void Init();
    }
    public class PlayerGameStats : IPlayerGameStatsRoot
    {
        #region Fields

        private ReactiveProperty<EnemyRequest> _enemyModel = new ReactiveProperty<EnemyRequest>();


        public IReadOnlyReactiveProperty<EnemyRequest> EnemyModel => _enemyModel;
        #endregion
        private readonly IDataCentralService _dataCentralService;
        private readonly ConfigManager _configManager;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public PlayerGameStats(IDataCentralService dataCentralService, ConfigManager configManager)
        {
            _dataCentralService = dataCentralService;
            _configManager = configManager;
        }
        public void Init()
        {
        }

        public void SetEnemyRequest(EnemyRequest enemyRequest)
        {
            _enemyModel.Value = enemyRequest;
        }
    }
}