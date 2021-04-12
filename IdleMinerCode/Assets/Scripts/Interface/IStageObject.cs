using Komastar.IdleMiner.Data;
using UnityEngine;

namespace Komastar.IdleMiner.Interface
{
    public interface IQueryable
    {
        void Query(IQueryRequest request);
    }

    public interface IQueryResponse
    {
        Vector3 Position { get; set; }
        int Amount { get; set; }
        CoinDO Coin { get; set; }
    }

    public interface IQueryRequest
    {
        int Power { get; set; }
    }
}
