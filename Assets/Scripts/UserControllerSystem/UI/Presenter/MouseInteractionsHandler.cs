using Abstractions;
using Abstractions.Commands;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControllSystem.UI.Model;
using Zenject;

namespace UserControllSystem
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private AttackableValue _attackablesRMB;
        [SerializeField] private Transform _groundTransform;

        [Inject] private CommandButtonsModel _model;
        public Action<ICommandExecutor> OnClick;
        ICommandExecutor[] _commands;
        ISelectable _selectable;

        private Plane _groundPlane;
        private Ray _ray;
        private RaycastHit[] _hits;

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
                return;

            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _hits = Physics.RaycastAll(_ray);

            if (Input.GetMouseButtonUp(0))
            {
                if (CheckHit<ISelectable>(_hits, out var selectable))
                {
                    _selectable = selectable;

                    _selectedObject.SetValue(_selectable);
                }
            }
            else
            {
                if (CheckHit<IAttackable>(_hits, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(_ray, out var enter) && _selectable.GetType().Name == "AllyUnit")
                {
                    _commands = (_selectedObject.CurrentValue as Component).GetComponentsInParent<ICommandExecutor>();
                    OnClick += _model.OnCommandButtonClicked;
                    OnClick?.Invoke(_commands[1]);
                    OnClick -= _model.OnCommandButtonClicked;
                    _groundClicksRMB.SetValue(_ray.origin + _ray.direction * enter);
                }
            }
        }
        public void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command)
        {
            commandExecutor.ExecuteCommand(command);
        }
        private bool CheckHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;

            if (hits.Length == 0)
            {
                return false;
            }

            result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .Where(c => c != null)
            .FirstOrDefault();

            return result != default;
        }
    }
}