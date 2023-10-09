using Models.DataModels;
using TMPro;
using Tools.Extensions;
using Tools.UiManager;
using UniRx;
using UnityEngine;
using VContainer;

namespace Ui.Windows
{
    public class TopPanelWindow : Window
    {
        [SerializeField] private TMP_Text _coinCountLabel;

        [Inject] private IDataCentralService _dataCentralService;
        
        protected override void OnActivate()
        {
            base.OnActivate();

            _dataCentralService.StatsDataModel.CoinsCount.
                Subscribe(value => _coinCountLabel.text = SetScoreExt.ConvertIntToStringValue(value, 1).ToString()).AddTo(ActivateDisposables);
        }
    }
}