using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UserControllSystem.CommandsRealization;
using Utils;
using Zenject;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IPatrolCommand> _creationCallback;

        private List<Vector3> v = new List<Vector3>();

        [Inject]
        private void Init(Vector3Value groundClicks) =>
            groundClicks.OnNewValue += OnNewValue;

        private void OnNewValue(Vector3 groundClick)
        {
            v.Add(groundClick);
        }

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback) =>
            _creationCallback = creationCallback;

        public override void ProcessCancel()
        {
            _creationCallback?.Invoke(_context.Inject(new PatrolCommand(v)));
            base.ProcessCancel();
            _creationCallback = null;
            v.Clear();
        }
    }
}