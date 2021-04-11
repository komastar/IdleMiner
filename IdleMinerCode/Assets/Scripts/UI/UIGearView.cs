using Komastar.IdleMiner.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UIGearView : MonoBehaviour
    {
        public UnityAction<GearDO> OnClickGearView;

        [SerializeField]
        private Text gearNameText;

        private GearDO gearData;

        public void Setup(GearDO gear)
        {
            gearData = gear;
            gearNameText.text = gear.Name;
        }

        public void OnClickGearButton()
        {
            OnClickGearView?.Invoke(gearData);
        }
    }
}
