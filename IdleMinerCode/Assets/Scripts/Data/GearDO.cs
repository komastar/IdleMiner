namespace Komastar.IdleMiner.Data
{
    [DOPath("Gear")]
    public struct GearDO : IDataObject
    {
        public int Id { get; set; }
        public string Name;
        public EGearType GearType;
        public StatusDO Status;

        public override string ToString()
        {
            return $"{Id} {Name} {GearType} {Status}";
        }
    }
}
