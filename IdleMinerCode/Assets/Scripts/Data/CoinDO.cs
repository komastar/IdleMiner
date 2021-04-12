namespace Komastar.IdleMiner.Data
{
    public enum EVeinSize
    {
        None = 0,
        Small,
        Normal,
        Large,
        Count
    }

    public enum ECoinType
    {
        BTC,
        ETH,
        XRP,
        Count
    }

    public struct CoinDO
    {
        public EVeinSize VeinSize;
        public ECoinType CoinType;
    }
}
