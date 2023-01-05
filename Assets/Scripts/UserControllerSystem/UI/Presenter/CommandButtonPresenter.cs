using Abstractions;
using Abstractions.Commands;
using System.Collections.Generic;
using UnityEngine;
using UserControllSystem.UI.Model;
using UserControllSystem.UI.View;
using Zenject;

namespace UserControllSystem.UI.Presenter
{
    public class CommandButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;

        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        private void Start()
        {

            _view.OnClick += _model.OnCommandButtonClicked;
            if (_model == null)
                Debug.Log(_model);
            else
                Debug.Log("NULL");
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
        }

        private void OnSelected(ISelectable selectable) 
        {

            if (_currentSelectable == selectable) 
            {
                return;
            }
            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }

            _currentSelectable = selectable;

            _view.Clear();
            if(selectable != null) 
            {
                List<ICommandExecutor> commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                ICommandsQueue queue = (selectable as Component).GetComponentInParent<ICommandsQueue>();
                _view.MakeLayout(commandExecutors, queue);

            }
        }
    }
}