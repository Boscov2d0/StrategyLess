using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;

namespace UserControllSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Unit1")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}