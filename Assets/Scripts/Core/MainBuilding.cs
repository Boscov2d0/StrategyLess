using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Core.CommandExecutor;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable, IAttackable
    {
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _maxHealth = 1000;

        private float _health = 1000;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public Vector3 RallyPoint { get; set; }

        public Transform PivotPoint => _unitsParent;

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command) =>
            Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
    }
}