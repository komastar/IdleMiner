using Komastar.IdleMiner.Interface;
using UnityEngine;

namespace Komastar.IdleMiner.Data
{
    public struct QueryResponse : IQueryResponse
    {
        public Vector3 Position { get; set; }
        public int Amount { get; set; }
        public CoinDO Coin { get; set; }
    }

    public struct QueryRequest : IQueryRequest
    {
        public int Power { get; set; }
    }
}
