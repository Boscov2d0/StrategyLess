using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine.AI;

namespace Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }
}