using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControllSystem.UI.Model;
using UserControllSystem.UI.Model.CommandCreator;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "UntitledInstaller", menuName = "Installers/UntitledInstaller")]
public class UntitledInstaller : ScriptableObjectInstaller<UntitledInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _vector3Value;
    [SerializeField] private EnemyValue _enemyValue;
    [SerializeField] private SelectableValue _selectables;

    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        Container.Bind<Vector3Value>().FromInstance(_vector3Value);
        Container.Bind<EnemyValue>().FromInstance(_enemyValue);
        Container.Bind<SelectableValue>().FromInstance(_selectables);

        Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
        Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();

        Container.Bind<CommandButtonsModel>().AsTransient();
    }
}