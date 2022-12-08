using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UserControllSystem.CommandsRealization;
using UserControllSystem.UI.View;
using Utils;

namespace UserControllSystem.UI.Presenter
{
    public class CommandButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += ONButtonClick;
        }

        private void OnSelected(ISelectable selectable) 
        {
            if (_currentSelectable == selectable) 
            {
                return;
            }
            _currentSelectable = selectable;

            _view.Clear();
            if(selectable != null) 
            {
                List<ICommandExecutor> commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
        private void ONButtonClick(ICommandExecutor commandExecutor) 
        {
            CommandExecutorBase<IProduceUnitCommand> unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null) 
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommandHeir()));
                return;
            }
            CommandExecutorBase<IAttackCommand> attack = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (attack != null)
            {
                attack.ExecuteSpecificCommand(_context.Inject(new AttackCommand()));
                return;
            }
            CommandExecutorBase<IHoldPositionCommand> stopper = commandExecutor as CommandExecutorBase<IHoldPositionCommand>;
            if (stopper != null)
            {
                stopper.ExecuteSpecificCommand(_context.Inject(new HoldPositionCommand()));
                return;
            }
            CommandExecutorBase<IMoveCommand> mover = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (mover != null)
            {
                mover.ExecuteSpecificCommand(_context.Inject(new MoveCommand()));
                return;
            }
            CommandExecutorBase<IPatrolCommand> patroller = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (patroller != null)
            {
                patroller.ExecuteSpecificCommand(_context.Inject(new PatrolCommand()));
                return;
            }
            throw new ApplicationException($"{nameof(CommandButtonPresenter)}.{nameof(ONButtonClick)} " 
                                         + $"Unknown type of command executor: {commandExecutor.GetType().FullName}!");
        }
    }
}