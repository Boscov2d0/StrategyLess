using Abstractions.Commands.CommandsInterfaces;
using System.Collections.Generic;
using UnityEngine;

namespace UserControllSystem.CommandsRealization
{
    public class PatrolCommand : IPatrolCommand
    {
        public List<Vector3> Target { get; }
        public PatrolCommand(List<Vector3> target)
        {
            Target = target;
        }
    }
}