using TMPro;
using Tools.Extensions;
using UnityEngine;

namespace Ui.Content.PlayResult
{
    public class ResultRewardBlock : MonoBehaviour
    {
        [SerializeField] private TMP_Text _labelValue;
        public void SetValue(int rewardCount)
        {
            gameObject.SetActive(rewardCount > 0);
            _labelValue.text = SetScoreExt.ConvertIntToStringValue(rewardCount, 1);
        }
    }
}