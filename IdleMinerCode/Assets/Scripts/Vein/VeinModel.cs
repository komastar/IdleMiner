using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Vein
{
    public class QueryEvent : UnityEvent<IQueryResponse> { }

    public class VeinModel
    {
        public static QueryEvent OnQueryDone;
        public static void Init()
        {
            if (null == OnQueryDone)
            {
                OnQueryDone = new QueryEvent();
            }
        }

        public UnityAction<int> OnChangeHp;

        public Transform ViewTransform;

        private CoinDO coinData;

        private int hp;
        public int Hp
        {
            get => hp;
            set
            {
                if (value != hp)
                {
                    hp = value;
                    if (hp < 0)
                    {
                        hp = 0;
                    }

                    OnChangeHp?.Invoke(hp);
                }
            }
        }

        public VeinModel(Transform transform)
        {
            ViewTransform = transform;
            coinData = new CoinDO()
            {
                VeinSize = EVeinSize.Small,
                CoinType = ECoinType.BTC
            };
        }

        public void Ready()
        {
            switch (coinData.VeinSize)
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

        public void Query(IQueryRequest request)
        {
            if (0 < Hp)
            {
                QueryResponse query = new QueryResponse()
                {
                    Position = ViewTransform.position,
                    Coin = coinData
                };

                switch (coinData.VeinSize)
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

                Hp -= request.Power;

                OnQueryDone?.Invoke(query);
            }
        }
    }
}