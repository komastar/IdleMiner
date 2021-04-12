using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Player
{
    public class CharacterView : MonoBehaviour
    {
        public UnityAction OnTriggerAnimEvent;

        public Animator characterAnimator;

        private void OnDestroy()
        {
            OnTriggerAnimEvent = null;
        }

        public void PlayQuery()
        {
            characterAnimator.Play("PlayerMining");
        }

        private void OnActionQuery()
        {
            OnTriggerAnimEvent?.Invoke();
        }
    }
}