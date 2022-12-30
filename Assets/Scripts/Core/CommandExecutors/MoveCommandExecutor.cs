using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine.AI;

namespace Core.CommandExecutor
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
        }
    }
}