using Komastar.IdleMiner.Coin;
using UnityEngine;

namespace Komastar.IdleMiner.Vein
{
    public class VeinPresenter : MonoBehaviour
    {
        [SerializeField]
        private VeinView view;
        private VeinModel model;

        [SerializeField]
        private CoinController coinCtrl;

        [SerializeField]
        private Transform coinSpringTransform;

        private void Awake()
        {
            model = VeinModel.Create();
            model.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Query();
            }
        }

        public int Query()
        {
            coinCtrl.CreateCoins(coinSpringTransform.position, 1);

            return model.Query();
        }
    }
}
