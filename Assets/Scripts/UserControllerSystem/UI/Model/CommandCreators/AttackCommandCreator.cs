using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UserControllSystem.CommandsRealization;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument) => 
            new AttackCommand(argument);
    }
}