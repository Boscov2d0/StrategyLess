using UnityEngine;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine.AI;
using System.Threading;
using Abstractions;
using Utils;

namespace Core.CommandExecutor
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            Vector3 point1 = command.From;
            Vector3 point2 = command.To;

            while (true)
            {
                GetComponent<NavMeshAgent>().destination = point2;
                _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();

                try
                {
                    await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    GetComponent<NavMeshAgent>().isStopped = true;
                    GetComponent<NavMeshAgent>().ResetPath();
                    break;
                }

                Vector3 temp = point1;
                point1 = point2;
                point2 = temp;
            }
            _stopCommandExecutor.CancellationTokenSource = null;
        }
    }
}