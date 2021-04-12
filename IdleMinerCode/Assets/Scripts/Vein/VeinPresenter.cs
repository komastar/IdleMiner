using Komastar.IdleMiner.Coin;
using Komastar.IdleMiner.Interface;
using UnityEngine;

namespace Komastar.IdleMiner.Vein
{
    public class VeinPresenter : MonoBehaviour, IPresenter
    {
        [SerializeField]
        private VeinView view;
        private VeinModel model;

        [SerializeField]
        private Transform coinSpringTransform;

        public CoinController CoinCtrl;

        public void Init()
        {
            model = new VeinModel(view.transform);
            model.OnChangeHp += OnChangeVeinHp;

            view.OnQuery += Query;
        }

        public void Ready()
        {
            model.Ready();
            view.TurnOn();
        }

        private void OnChangeVeinHp(int hp)
        {
            if (hp <= 0)
            {
                view.TurnOff();
            }
        }

        public void Query(IQueryRequest request)
        {
            model.Query(request);
        }
    }
}
