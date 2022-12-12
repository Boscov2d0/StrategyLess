using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace UserControllSystem.CommandsRealization
{
    public class AttackCommand : IAttackCommand
    {
        public IEnemy Target { get; }
        public AttackCommand(IEnemy target)
        {
            Target = target;
        }
    }
}