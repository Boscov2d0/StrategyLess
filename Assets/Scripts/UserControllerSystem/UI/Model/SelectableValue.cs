using System;
using Abstractions;
using UnityEngine;

namespace UserControllSystem
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy game/" + nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}