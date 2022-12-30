using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
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

        private Dictionary<Type, Button> _buttonsByExecutorType;

        public Action<ICommandExecutor> OnClick;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();

            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
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
        }
        public void MakeLayout(List<ICommandExecutor> commandExecutors)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                Button buttonGameObject = GetButtonGameObjectByType(currentExecutor.GetType());
                buttonGameObject.gameObject.SetActive(true);
                buttonGameObject.onClick.AddListener(() =>
                OnClick?.Invoke(currentExecutor));
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