namespace Komastar.IdleMiner.Data
{
    [DOPath("Enemy")]
    public struct EnemyDO : IDataObject
    {
        public int Id { get; set; }
        public string Name;
        public int StatusId;
        public int Exp;
    }
}