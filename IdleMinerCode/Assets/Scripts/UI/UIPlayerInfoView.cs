using UnityEngine;

namespace Komastar.IdleMiner.UI.Player
{
    public class UIPlayerInfoView : MonoBehaviour
    {
        public UILabelTextView LevelView;
        public UILabelTextView ExpView;
        public UILabelTextView AtkView;

        public void OnLevelUp(int level)
        {
            LevelView.SetValue(level);
        }

        public void OnEarnExp(int exp, int maxExp)
        {
            ExpView.SetValue(exp, maxExp);
        }

        public void OnChangeAtk(int atk)
        {
            AtkView.SetValue(atk);
        }
    }
}