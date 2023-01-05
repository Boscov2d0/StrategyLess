using UnityEngine;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;

namespace Core.CommandExecutor
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} patroling to {command}!");
        }
    }
}