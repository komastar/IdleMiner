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

        public void Equip(GearDO gear)
        {
            weaponView.Setup(gear);
            playerPresenter.EquipGear(gear);
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
                    Equip(gear.Data);
                }
            }
        }
    }
}