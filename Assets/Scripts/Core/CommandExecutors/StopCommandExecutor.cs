using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutor
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"{name} has stoped!");
        }
    }
}