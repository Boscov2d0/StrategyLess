using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UnityEngine;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(EnemyValue groundClicks) =>
            groundClicks.OnNewValue += OnNewValue;

        private void OnNewValue(IEnemy groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(groundClick)));
            _creationCallback = null;
        }
        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback) =>
            _creationCallback = creationCallback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}