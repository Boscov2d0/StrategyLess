using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine.AI;

namespace Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }
}