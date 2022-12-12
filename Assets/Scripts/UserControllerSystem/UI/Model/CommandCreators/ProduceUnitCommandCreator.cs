using Abstractions.Commands.CommandsInterfaces;
using System;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;
        protected override void
        ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new
            ProduceUnitCommandHeir()));
        }
    }
}