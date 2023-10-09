using System;

namespace Models.SO.Core
{
    [Serializable]
    public struct StatsTestRestConfig
    {
        public string EnemyURL;
        public MinMaxConfigValues EnemyHealth;
        public MinMaxConfigValues PlayerDamage;
        public MinMaxConfigValues BattleReward;
    }

    [Serializable]
    public struct MinMaxConfigValues
    {
        public int MinValue;
        public int MaxValue;
    }
}