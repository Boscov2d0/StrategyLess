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
        [SerializeField] private Button _holdPositionButton;
        [SerializeField] private Button _produceUnitButton;

        private Dictionary<Type, Button> _buttonsByExecutorType;

        public Action<ICommandExecutor> OnClick;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, Button>();

            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IHoldPositionCommand>), _holdPositionButton);
            _buttonsByExecutorType.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
        }
        public void MakeLayout(List<ICommandExecutor> commandExecutors)
        {
            /*
            for (int i = 0; i < commandExecutors.Count; i++)
            {
                Button buttonGameObject = _buttonsByExecutorType
                    .Where(type => type
                    .Key
                    .IsAssignableFrom(commandExecutors[i].GetType()))
                    .First()
                    .Value;

                buttonGameObject.gameObject.SetActive(true);
                Button button = buttonGameObject;
                button.onClick.AddListener(() => OnClick?.Invoke(commandExecutors[i]));
            }*/
            foreach (var currentExecutor in commandExecutors)
            {
                Button buttonGameObject = _buttonsByExecutorType
                .Where(type => type
                .Key
                .IsAssignableFrom(currentExecutor.GetType()))
                .First()
                .Value;
                buttonGameObject.gameObject.SetActive(true);
                Button button = buttonGameObject;
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
            }
        }
        public void Clear()
        {
            /*
            for (int i = 0; i < _buttonsByExecutorType.Count; i++) 
            {
                _buttonsByExecutorType[i].Value.GetComponent<Button>().onClick.RemoveAllListeners();
                _buttonsByExecutorType[i].Value.gameObject.SetActive(false);
            }*/
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.gameObject.SetActive(false);
            }
        }
    }
}