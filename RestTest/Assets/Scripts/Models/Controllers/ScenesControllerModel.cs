using Tools.UiManager;
using RestTest.MultiScene;
using RestTest.StateMachine;
using Ui.Windows;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace Models.Controllers
{
    public class ScenesControllerModel : IInitializable
    {
        private ScenesStateEnum _scene = ScenesStateEnum.Base;
        [Inject] private readonly IMultiSceneManager _multiSceneManager;
        [Inject] private readonly ICoreStateMachine _coreStateMachine;
        [Inject] private readonly IWindowManager _windowManager;
        private FadeWindow _fadeWindow;
        
        public void Initialize()
        {
            _coreStateMachine.ScenesState.SkipLatestValueOnSubscribe().Subscribe(CurrentSceneSwitches);
        }
        private void CurrentSceneSwitches(ScenesStateEnum scene)
        {
            if (_fadeWindow == null)
            {
                _fadeWindow = _windowManager.GetWindow<FadeWindow>();
            }
            _windowManager.Show(_fadeWindow, WindowPriority.LoadScene);
            _fadeWindow.CloseFade(EndCloseFade);
            _scene = scene;
        }

        private void EndCloseFade()
        {
            _coreStateMachine.OnSceneEndLoad();
            _multiSceneManager.LoadScene(_scene, NextSceneEndLoad);
        }
        
        private void EndOpenFade() => _coreStateMachine.OnSceneEndLoadFade();
        
        private void NextSceneEndLoad()
        {
            _multiSceneManager.UnloadLastScene();
            _multiSceneManager.SetActiveLastLoadScene();
            _fadeWindow.OpenFade(EndOpenFade);
        }
    }
}