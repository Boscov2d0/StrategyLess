using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControllSystem.CommandsRealization;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class PatrolCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private SelectableValue _selectable;
        protected override IPatrolCommand CreateCommand(Vector3 argument) => 
            new PatrolCommand(_selectable.CurrentValue.PivotPoint.position, argument);
    }
}