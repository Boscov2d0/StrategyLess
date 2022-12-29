using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControllSystem.CommandsRealization
{
    public class AttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }
        public AttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}