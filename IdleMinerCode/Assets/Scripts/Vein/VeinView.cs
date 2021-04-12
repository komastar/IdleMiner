using Komastar.IdleMiner.Interface;
using System;
using UnityEngine;

namespace Komastar.IdleMiner.Vein
{
    public class VeinView : MonoBehaviour, IInteractable
    {
        public Func<IInteractResult> OnInteract;

        public IInteractResult Interact()
        {
            return OnInteract?.Invoke();
        }
    }
}