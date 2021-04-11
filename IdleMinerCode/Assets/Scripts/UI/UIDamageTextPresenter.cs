using System.Collections.Generic;
using UnityEngine;

namespace Komastar.IdleMiner.UI
{
    public class UIDamageTextPresenter : MonoBehaviour
    {
        private static UIDamageTextPresenter instance;
        public static UIDamageTextPresenter Get()
        {
            if (ReferenceEquals(null, instance))
            {
                instance = FindObjectOfType<UIDamageTextPresenter>();
            }

            return instance;
        }

        private Queue<UIDamageTextView> pool;
        private Camera cam;

        [SerializeField]
        private UIDamageTextView damageTextViewPrefab;

        private void Awake()
        {
            instance = this;

            pool = new Queue<UIDamageTextView>();
            cam = Camera.main;
        }

        public UIDamageTextView Rent(Vector3 position)
        {
            position = cam.WorldToScreenPoint(position);

            UIDamageTextView rent;
            if (0 < pool.Count)
            {
                rent = pool.Dequeue();
                rent.transform.position = position;
            }
            else
            {
                rent = Instantiate(damageTextViewPrefab, position, Quaternion.identity, transform);
            }

            rent.TurnOff();

            return rent;
        }

        public void Return(UIDamageTextView damageTextView)
        {
            damageTextView.TurnOff();
            pool.Enqueue(damageTextView);
        }
    }
}