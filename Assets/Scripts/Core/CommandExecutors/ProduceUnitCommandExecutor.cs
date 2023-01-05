using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.CommandExecutor
{

    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        //UnitProductionTask _innerTask;

        private void Start() => Observable.EveryUpdate().Subscribe(l => OnUpdate());

        private void OnUpdate()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var _innerTask = (UnitProductionTask)_queue[0];
            _innerTask.TimeLeft -= Time.deltaTime;

            if (_innerTask.TimeLeft <= 0)
            {
                removeTaskAtIndex(0);
                Instantiate(_innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
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
            if (_queue.Count == _maximumUnitsInQueue)
                return;

            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }
    }
}