using Abstractions.Commands;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutor
{

    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<T> where T : class, ICommand
    {
        public async Task TryExecuteCommand(object command)
        {
            var specificCommand = command as T;
            if (specificCommand != null)
            {
                await ExecuteSpecificCommand(specificCommand);
            }
        }
        public abstract Task ExecuteSpecificCommand(T command);
    }
}