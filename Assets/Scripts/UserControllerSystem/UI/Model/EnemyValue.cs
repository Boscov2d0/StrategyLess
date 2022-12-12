using Abstractions;
using UnityEngine;

namespace UserControllSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(EnemyValue), menuName = "Strategy game/" + nameof(EnemyValue), order = 0)]
    public class EnemyValue : ObjectValueBase<IEnemy> { }
}