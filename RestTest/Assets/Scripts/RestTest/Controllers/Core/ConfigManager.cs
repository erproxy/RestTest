using SO.Core;
using UnityEngine;

namespace RestTest.Controllers.Core
{
    public class ConfigManager : MonoBehaviour
    {
        [field: SerializeField] public StatsSO StatsSO { get; private set; }
    }
}