using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutor
{
    public class HoldPositionCommandExecutor : CommandExecutorBase<IHoldPositionCommand>
    {
        public override void ExecuteSpecificCommand(IHoldPositionCommand command)
        {
            Debug.Log($"{name} has hold position!");
        }
    }
}