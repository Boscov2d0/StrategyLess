using Abstractions;
using Abstractions.Commands;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControllSystem.UI.Model;
using Zenject;

namespace UserControllSystem.UI.Presenter
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private AttackableValue _attackablesRMB;
        [SerializeField] private Transform _groundTransform;
        [SerializeField] private EventSystem _eventSystem;

        [Inject] private CommandButtonsModel _model;
        public Action<ICommandExecutor> OnClick;
        ICommandExecutor[] _commands;

        private Plane _groundPlane;

        [Inject]
        private void Init()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);

            var nonBlockedByUiFramesStream = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());

            var leftClicksStream = nonBlockedByUiFramesStream.Where(_ => Input.GetMouseButtonDown(0));
            var rightClicksStream = nonBlockedByUiFramesStream.Where(_ => Input.GetMouseButtonDown(1));

            var lmbRays = leftClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var rmbRays = rightClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

            var lmbHitsStream = lmbRays.Select(ray => Physics.RaycastAll(ray));

            var rmbHitsStream = rmbRays.Select(ray => (ray, Physics.RaycastAll(ray)));

            lmbHitsStream.Subscribe(hits =>
            {
                if (CheckHit<ISelectable>(hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            });

            rmbHitsStream.Subscribe(data =>
            {
                var (ray, hits) = data;
                if (CheckHit<IAttackable>(hits, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter) && _selectedObject.CurrentValue.GetType().Name == "AllyUnit")
                {
                    _commands = (_selectedObject.CurrentValue as Component).GetComponentsInParent<ICommandExecutor>();
                    OnClick += _model.OnCommandButtonClicked;
                    OnClick?.Invoke(_commands[1]);
                    OnClick -= _model.OnCommandButtonClicked;
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            });
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