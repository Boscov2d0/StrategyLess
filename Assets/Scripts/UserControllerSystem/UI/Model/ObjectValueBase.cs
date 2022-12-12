using System;
using UnityEngine;

namespace UserControllSystem.UI.Model
{
    public class ObjectValueBase<T> : ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        public void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}