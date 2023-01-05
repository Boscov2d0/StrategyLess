namespace Abstractions
{
    public interface IUnitProductionTask : IIconHolder
    {
        public string UnitName { get; }
        public float TimeLeft { get; set; }
        public float ProductionTime { get; }
    }
}