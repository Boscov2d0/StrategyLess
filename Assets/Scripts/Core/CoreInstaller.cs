using Abstractions;
using Zenject;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();

            Container.Bind<IGameStatus>().FromInstance(this.gameObject.GetComponent<GameStatus>());
        }
    }
}