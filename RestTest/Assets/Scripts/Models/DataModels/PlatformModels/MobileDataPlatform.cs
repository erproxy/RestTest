using Models.DataModels.Data;
using Tools.GameTools;

namespace Models.DataModels.PlatformModels
{
    public class MobileDataPlatform : AbstractDataPlatform
    {
        private JsonSerialization<StatsData> _jsonSerializationStatsData = new((PRENAME + nameof(StatsDataModel) + ".json"));

        #region Save
        public override void SaveStatsDataModel() => _jsonSerializationStatsData.Serialization(StatsDataModel.GetStatsData());

        #endregion
        
        #region Load
        public override void GetData()
        {
            LoadStatsDataModel(_jsonSerializationStatsData.DeSerialization());
        }
        
        private void LoadStatsDataModel((bool result, StatsData statsData) result)
        {
            if (result.result)
                StatsDataModel.SetStatsData(result.statsData);
            else
                StatsDataModel.SetAndInitEmptyStatsData(new StatsData());

            Debugger.LogBold((PRENAME + nameof(StatsDataModel)) + "EndLoad");
            
            IsStatsDataModelLoaded = true;
        }
    #endregion
    }
}