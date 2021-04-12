using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using UnityEngine;

namespace Komastar.IdleMiner.Vein
{
    public enum EVeinSize
    {
        None = 0,
        Small,
        Normal,
        Large,
        Count
    }

    public enum EVeinType
    {
        BTC,
        ETH,
        XRP,
        Count
    }

    public class VeinModel
    {
        public EVeinSize VeinSize;
        public EVeinType VeinType;
        public int Hp;

        public static VeinModel Create()
        {
            return new VeinModel
            {
                VeinSize = (EVeinSize)Random.Range((int)EVeinSize.None, (int)EVeinSize.Count),
                VeinType = EVeinType.BTC
            };
        }

        public void Init()
        {
            switch (VeinSize)
            {
                case EVeinSize.Small:
                    Hp = 1;
                    break;
                case EVeinSize.Normal:
                    Hp = 3;
                    break;
                case EVeinSize.Large:
                    Hp = 7;
                    break;
            }
        }

        public IInteractResult Query()
        {
            QueryDO query = new QueryDO();
            switch (VeinSize)
            {
                case EVeinSize.Small:
                    query.Amount = Random.Range(1, 3);
                    break;
                case EVeinSize.Normal:
                    query.Amount = Random.Range(2, 5);
                    break;
                case EVeinSize.Large:
                    query.Amount = Random.Range(3, 7);
                    break;
                case EVeinSize.None:
                case EVeinSize.Count:
                default:
                    query.Amount = 1;
                    break;
            }

            return query;
        }
    }
}