using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControllSystem.CommandsRealization;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateCommand(Vector3 argument) => 
            new MoveCommand(argument);
    }
}