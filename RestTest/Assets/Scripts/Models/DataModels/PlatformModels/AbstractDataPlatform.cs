using Models.DataModels.Models;

namespace Models.DataModels.PlatformModels
{
    public class AbstractDataPlatform
    {

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        protected const string PRENAME= "RestTestStorageDev";
#else
        protected const string PRENAME= "RestTestStorage";
#endif
        public bool IsStatsDataModelLoaded = false;
        
        protected StatsDataModel StatsDataModel;
        
        public void Init(StatsDataModel statsDataModel)
        {
            StatsDataModel = statsDataModel;
        }
        
        public void SaveFullData()
        {
            SaveStatsDataModel();
        }

        public virtual void SaveStatsDataModel()
        {
        }

        public virtual void GetData()
        {
        }
    }
}