using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UserControllSystem.UI.View
{
    public class CommandButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _produceUnitButton;
        [SerializeField] private Button _setRallyPointButton;

        private Dictionary<Type, Button> _buttonsByExecutorType;

        public Action<ICommandExecutor, ICommandsQueue> OnClick;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();

            _buttonsByExecutorType.Add(typeof(ICommandExecutor< IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(ICommandExecutor<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(ICommandExecutor<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(ICommandExecutor<IStopCommand>), _stopButton);
            _buttonsByExecutorType.Add(typeof(ICommandExecutor<IProduceUnitCommand>), _produceUnitButton);
            _buttonsByExecutorType.Add(typeof(ICommandExecutor<ISetRallyPointCommand>), _setRallyPointButton);
        }
        public void BlockInteractions(ICommandExecutor ce)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(ce.GetType()).GetComponent<Selectable>().interactable = false;
        }
        public void UnblockAllInteractions() => SetInteractible(true);
        private void SetInteractible(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
            _setRallyPointButton.GetComponent<Selectable>().interactable = value;
        }
        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors, ICommandsQueue queue)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                Button buttonGameObject = GetButtonGameObjectByType(currentExecutor.GetType());
                buttonGameObject.gameObject.SetActive(true);
                buttonGameObject.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
            }
        }
        private Button GetButtonGameObjectByType(Type executorInstanceType)
        {
            /*
            return _buttonsByExecutorType
            .Where(type =>
            type.Key.IsAssignableFrom(executorInstanceType))
            .First()
            .Value;
            */
            return _buttonsByExecutorType.First(type => type.Key.IsAssignableFrom(executorInstanceType)).Value;
        }
        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.gameObject.SetActive(false);
            }
        }
    }
}