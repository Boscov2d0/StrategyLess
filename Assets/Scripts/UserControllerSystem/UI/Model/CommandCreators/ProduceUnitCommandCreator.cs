using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Threading.Tasks;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public sealed class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private DiContainer _diContainer;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            ProduceUnitCommandHeir produceUnitCommand = _context.Inject(new ProduceUnitCommandHeir());
            _diContainer.Inject(produceUnitCommand);
            creationCallback?.Invoke(produceUnitCommand);
        }
    }
}