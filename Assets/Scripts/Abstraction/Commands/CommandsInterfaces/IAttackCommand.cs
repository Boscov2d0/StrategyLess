using UnityEngine;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IAttackCommand : ICommand
    {
        public IEnemy Target { get; }
    }
}