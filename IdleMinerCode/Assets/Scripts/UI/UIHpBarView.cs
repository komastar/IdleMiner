using UnityEngine;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UIHpBarView : MonoBehaviour
    {
        public Image hpBar;
        public Text hpText;

        public void SetValue(int v1, int v2)
        {
            hpText.text = $"{v1} / {v2}";

            float ratio = v1 / (float)v2;
            hpBar.fillAmount = ratio;
        }
    }
}