using Abstractions;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserControllSystem
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        private RaycastHit[] _hits;
        private ISelectable _selectable;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!Input.GetMouseButtonUp(0)) 
                return;

            _hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

            if (_hits.Length == 0)
                return;

            _selectable = _hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();

            _selectedObject.SetValue(_selectable);
        }
    }
}