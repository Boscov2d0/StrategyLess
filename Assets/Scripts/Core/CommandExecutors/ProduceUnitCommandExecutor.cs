using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;
using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using Abstractions.Commands;
using System.Threading.Tasks;

namespace Core.CommandExecutor
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        private UnitProductionTask _innerTask;
        private GameObject _instance;
        private FactionMember _factionMember;

        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            _innerTask = (UnitProductionTask)_queue[0];
            _innerTask.TimeLeft -= Time.deltaTime;

            if (_innerTask.TimeLeft <= 0)
            {
                removeTaskAtIndex(0);
                _instance = Instantiate(_innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
                _instance.transform.position += new Vector3(_instance.transform.position.x, 0.5f, _instance.transform.position.z);
                _factionMember = _instance.GetComponent<FactionMember>();
                _factionMember.SetFaction(GetComponent<FactionMember>().FactionId);
            }
        }
        public void Cancel(int index) => removeTaskAtIndex(index);
        private void removeTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count - 1);
        }
        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }

    }
}