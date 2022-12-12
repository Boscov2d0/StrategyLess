using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutor
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            for (int i = 0; i < command.Target.Count; i++)
            {
                Debug.Log($"{name} patroling to {command.Target[i]}!");
            }
        }
    }
}