using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Player
{
    public class CharacterView : MonoBehaviour
    {
        public UnityAction OnAttack;

        public Animator characterAnimator;

        public void PlayAttack()
        {
            characterAnimator.Play("PlayerMining");
        }

        private void OnAttackAction()
        {
            OnAttack?.Invoke();
        }
    }
}