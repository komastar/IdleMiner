using UnityEngine;
using UnityEngine.UI;

namespace Komastar.IdleMiner.UI
{
    public class UILabelTextView : MonoBehaviour
    {
        [SerializeField]
        private Text labelText;
        [SerializeField]
        private Text valueText;

        public void SetLabel(string label)
        {
            labelText.text = label;
        }

        public void SetValue(string value)
        {
            valueText.text = value;
        }

        public void SetValue(int value)
        {
            valueText.text = $"{value}";
        }

        public void SetValue(int v1, int v2)
        {
            valueText.text = $"{v1} / {v2}";
        }
    }
}