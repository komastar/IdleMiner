using Komastar.IdleMiner.Interface;

namespace Komastar.IdleMiner.Data
{
    public struct QueryDO : IInteractResult
    {
        public int Id { get; set; }
        public int Amount { get; set; }
    }
}
