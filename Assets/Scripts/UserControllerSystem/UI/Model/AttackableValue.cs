using Abstractions;
using UnityEngine;

namespace UserControllSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy game/" + nameof(AttackableValue), order = 0)]
    public class AttackableValue : ObjectValueBase<IAttackable> { }
}