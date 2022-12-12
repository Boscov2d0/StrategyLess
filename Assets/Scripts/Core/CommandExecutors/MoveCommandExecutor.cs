using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutor
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} is moving to {command.Target}!");
        }
    }
}