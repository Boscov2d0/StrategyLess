using Abstractions.Commands.CommandsInterfaces;
using System;
using UnityEngine;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IStopCommand> _creationCallback;

        [Inject]
        private void Init(Vector3Value groundClicks) =>
            groundClicks.OnNewValue += OnNewValue;

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new StopCommand()));
            ProcessCancel();
        }
        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback) =>
            _creationCallback = creationCallback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }
    }
}