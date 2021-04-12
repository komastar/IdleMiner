using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Player;
using UnityEngine;

namespace Komastar.IdleMiner.UI
{
    public class UIEquipPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIEquipButtonView weaponView;
        [SerializeField]
        private PlayerPresenter playerPresenter;
        [SerializeField]
        private UIGearPresenter uiGearPresenter;

        private void Awake()
        {
            weaponView.OnClickView += OnClickEquipButton;
        }

        public async void OnClickEquipButton()
        {
            if (uiGearPresenter.IsOpen)
            {
                uiGearPresenter.Close();
            }
            else
            {
                UISelectResponse<GearDO> gear = await uiGearPresenter.Open();
                if (null != gear)
                {
                }
            }
        }
    }
}