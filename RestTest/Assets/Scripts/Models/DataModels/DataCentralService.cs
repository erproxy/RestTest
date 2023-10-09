using Cysharp.Threading.Tasks;
using Models.DataModels.Data;
using Models.DataModels.Models;
using Models.DataModels.PlatformModels;

namespace Models.DataModels
{
    public interface IDataCentralService
    {
        IStatsDataModel StatsDataModel { get; }
        void SaveFull();
        void SaveStatsDataModel();
    }
    
    public class DataCentralService: IDataCentralService
    {
        private StatsDataModel _statsDataModel = new StatsDataModel();
        public IStatsDataModel StatsDataModel => _statsDataModel;
        private AbstractDataPlatform _dataPlatform;

        #region PublicMethods
        
        public DataCentralService()
        {
            _dataPlatform = new MobileDataPlatform();
            
            _dataPlatform.Init(_statsDataModel);
        }

        public void SaveStatsDataModel()
        {
            _dataPlatform.SaveStatsDataModel();
        }
        
        public void SaveFull()
        {
            SaveStatsDataModel();
        }

        public void Restart()
        {
          _statsDataModel.SetAndInitEmptyStatsData(new StatsData());
          SaveFull();
        }
        
        public async UniTask LoadData()
        {
            _dataPlatform.GetData();
            await UniTask.WaitUntil(() =>  _dataPlatform.IsStatsDataModelLoaded);
        }
        #endregion
    }
}