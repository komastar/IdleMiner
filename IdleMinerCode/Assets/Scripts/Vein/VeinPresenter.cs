using Komastar.IdleMiner.Coin;
using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Interface;
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

            view.OnInteract += Interact;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Interact();
            }
        }

        public IInteractResult Interact()
        {
            var result = model.Query();
            coinCtrl.CreateCoins(coinSpringTransform.position, result.Amount);

            return result;
        }
    }
}
