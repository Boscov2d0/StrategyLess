using Abstractions;
using System.Linq;
using UnityEngine;

namespace UserControlSystem
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        private RaycastHit[] _hits;
        private ISelectable _selectableCurrent;
        private ISelectable _selectable;
        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
                return;

            _hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

            if (_hits.Length == 0)
                return;

            _selectable = _hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();

            if (_selectableCurrent != null && _selectable != _selectableCurrent)
                    _selectableCurrent.IsSelect(false);

            _selectableCurrent = _selectable;

            if (_selectableCurrent != null)
            {
                _selectableCurrent.IsSelect(true);
                _selectedObject.SetValue(_selectableCurrent);
            }
        }
    }
}