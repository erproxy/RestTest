using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class EnemyUnitView : MonoBehaviour
    {
        //[SerializeField] private DOTweenAnimation _hitTween;
        [SerializeField] private Slider _healthBar;

        public void SetHealthBar(float value)
        {
            _healthBar.value = value;
        }
        
        public void SetHit()
        {
        //    _hitTween.DORestart();
        }
    }
}