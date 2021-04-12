using Komastar.IdleMiner.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UIEquipButtonView : MonoBehaviour
    {
        [SerializeField]
        private Text gearNameText;

        private int gearId;

        public UnityAction OnClickView;

        public void Setup(GearDO gearData)
        {
            if (gearId != gearData.Id)
            {
                gearId = gearData.Id;
                gearNameText.text = gearData.Name;
            }
        }

        public void OnClickButton()
        {
            OnClickView?.Invoke();
        }
    }
}
