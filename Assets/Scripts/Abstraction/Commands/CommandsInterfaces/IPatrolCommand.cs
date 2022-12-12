using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IPatrolCommand : ICommand 
    {
        public List<Vector3> Target { get; }
    }
}