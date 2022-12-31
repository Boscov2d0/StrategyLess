using Abstractions;
using UnityEngine;

namespace UserControllSystem.UI.View
{
    public class OutlineSelector : MonoBehaviour
    {
        //[SerializeField] private MeshRenderer[] _renderers;
        //[SerializeField] private Material _outlineMaterial;
        [SerializeField] private Outline _outline;

        private bool _isSelectedCache;

        public void SetSelected(bool isSelected)
        {
            if (isSelected == _isSelectedCache)
            {
                return;
            }

            _outline.enabled = isSelected;
            /*
            for (int i = 0; i < _renderers.Length; i++)
            {
                MeshRenderer renderer = _renderers[i];
                List<Material> materialsList = renderer.materials.ToList();
                if (isSelected)
                {
                    materialsList.Add(_outlineMaterial);
                }
                else
                {
                    materialsList.RemoveAt(materialsList.Count - 1);
                }
                renderer.materials = materialsList.ToArray();
            }*/
            _isSelectedCache = isSelected;
        }
    }
}