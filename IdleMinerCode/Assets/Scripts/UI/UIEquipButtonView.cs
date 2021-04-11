using Komastar.IdleMiner.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UIEquipButtonView : MonoBehaviour
    {
        [SerializeField]
        private Text gearNameText;

        private int gearId;

        public void Setup(GearDO gearData)
        {
            if (gearId != gearData.Id)
            {
                gearId = gearData.Id;
                gearNameText.text = gearData.Name;
            }
        }
    }
}
