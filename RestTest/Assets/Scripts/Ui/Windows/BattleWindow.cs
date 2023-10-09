using Tools.UiManager;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class BattleWindow : Window
    {
        [SerializeField] private Button _inputButton;

        public Button InputButton => _inputButton;
    }
}