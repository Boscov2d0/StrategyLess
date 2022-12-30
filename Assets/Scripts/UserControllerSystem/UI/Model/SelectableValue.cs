using Abstractions;
using UnityEngine;

namespace UserControllSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy game/" + nameof(SelectableValue), order = 0)]
    public class SelectableValue : ObjectValueBase<ISelectable> {}
}