using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControllSystem.CommandsRealization;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) => new SetRallyPointCommand(argument);
    }
}