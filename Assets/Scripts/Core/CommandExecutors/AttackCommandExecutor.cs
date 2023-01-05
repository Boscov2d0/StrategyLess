using UnityEngine;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;

namespace Core.CommandExecutor
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} is attacking {command.Target}!");
        }
    }
}