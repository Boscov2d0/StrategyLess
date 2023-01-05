using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Core.CommandExecutor;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject]
        CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        public void Clear() { }
        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
        }
    }
}