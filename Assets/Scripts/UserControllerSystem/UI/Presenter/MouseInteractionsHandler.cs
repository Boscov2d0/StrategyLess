using Abstractions;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControllSystem.UI.Model;

namespace UserControllSystem
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private EnemyValue _enemyValue;
        [SerializeField] private Transform _groundTransform;

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
            if (_hits.Length == 0)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (CheckHit<ISelectable>(_hits, out var selectable))
                {
                    _selectedObject.SetValue(selectable);
                }
            }
            else
            {
                if (CheckHit<IEnemy>(_hits, out var enemy))
                {
                    _enemyValue.SetValue(enemy);
                }
                else if (_groundPlane.Raycast(_ray, out var enter))
                {
                    _groundClicksRMB.SetValue(_ray.origin + _ray.direction * enter);
                }
            }
        }

        private bool CheckHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;

            if (hits.Length == 0)
            {
                Debug.Log("false");
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