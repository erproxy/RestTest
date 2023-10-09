using Models.SO.Core;
using UnityEngine;

namespace SO.Core
{
    [CreateAssetMenu(fileName = "StatsSO", menuName = "MyAssets/Config/StatsSO", order = 5)]
    public class StatsSO : ScriptableObject
    {
        [field: SerializeField] public StatsTestRestConfig StatsTestRestConfig { get; private set; }
    }
}