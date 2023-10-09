using Models.Controllers;
using Models.DataModels;
using Models.Fabrics;
using Models.WebTool;
using RestTest.Controllers.Core;
using RestTest.MultiScene;
using RestTest.StateMachine;
using Tools.UiManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RestTest.Controllers.LifeTimeScopes
{
    public class MainLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(Object.FindObjectOfType<WindowManager>()).As<IWindowManager>();
            builder.RegisterComponent(Object.FindObjectOfType<ConfigManager>()).As<ConfigManager>();

            builder.RegisterEntryPoint<ScenesControllerModel>();
            
            builder.Register<PrefabInject>(Lifetime.Singleton);
            builder.Register<ICoreStateMachine, CoreStateMachine>(Lifetime.Singleton);
            builder.Register<PlayerGameStats>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WebToolService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IMultiSceneManager, MultiSceneManager>(Lifetime.Singleton);
            builder.Register<DataCentralService>(Lifetime.Singleton).As<IDataCentralService, DataCentralService>();
        }
    }
}