using System;
using Cysharp.Threading.Tasks;
using Tools.GameTools;
using Tools.UiManager;
using UnityEngine;

namespace Ui.Windows
{
    public class FadeWindow : Window
    {
       // [SerializeField] private DOTweenAnimation _doTweenAnimation;

        private Action _fadeSceneDelegate;
        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _fadeSceneDelegate = null;
        }

        public async UniTask OpenFade(Action endBack = null)
        {
            _fadeSceneDelegate = endBack;
            await UniTask.Delay(1000);
            EndOpenFade();
            // _doTweenAnimation.DORestartById(StringsHelper.Helper.Open);
            // _doTweenAnimation.DOPlayById(StringsHelper.Helper.Open);
        }

        public async UniTask CloseFade(Action endBack = null)
        {
            _fadeSceneDelegate = endBack;
            await UniTask.Delay(1000);
            EndCloseFade();
            // _doTweenAnimation.DORestartById(StringsHelper.Helper.Close);
            // _doTweenAnimation.DOPlayById(StringsHelper.Helper.Close);
        }
        
        private void EndOpenFade()
        {
            _fadeSceneDelegate?.Invoke();
            Debug.Log("Open scene Fade ended");
            _manager.Hide(this);
        }
        
        private void EndCloseFade()
        {
            _fadeSceneDelegate?.Invoke();
            Debug.Log("Close scene Fade ended");
        }
    }
}