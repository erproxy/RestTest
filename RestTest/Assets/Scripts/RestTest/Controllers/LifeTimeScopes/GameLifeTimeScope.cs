using VContainer;
using VContainer.Unity;

namespace RestTest.Controllers.LifeTimeScopes
{
    public class GameLifeTimeScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}