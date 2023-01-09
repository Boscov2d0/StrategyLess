using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Threading.Tasks;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;

        private int _timeOfCreateCommand = 500;

        protected override async void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            await Task.Delay(_timeOfCreateCommand);
            creationCallback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));
        }
    }
}