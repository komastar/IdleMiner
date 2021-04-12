using Komastar.IdleMiner.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace Komastar.IdleMiner.Vein
{
    public class VeinView : MonoBehaviour, IQueryable, IView
    {
        public UnityAction<IQueryRequest> OnQuery;

        public void Query(IQueryRequest request)
        {
            OnQuery?.Invoke(request);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
        }
    }
}