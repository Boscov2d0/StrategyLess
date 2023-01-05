using Abstractions;
using UnityEngine;

namespace Core 
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public GameObject UnitPrefab { get; }
        public Sprite Icon { get; }
        public float TimeLeft { get; set; }
        public float ProductionTime { get; }
        public string UnitName { get; }

        public UnitProductionTask(float time, Sprite icon, GameObject unitPrefab, string unitName)
        {
            UnitPrefab = unitPrefab;
            Icon = icon;
            ProductionTime = time;
            TimeLeft = time;
            UnitName = unitName;
        }
    }
}