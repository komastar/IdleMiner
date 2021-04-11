using Cysharp.Threading.Tasks;
using DG.Tweening;
using Komastar.IdleMiner.Data;
using Komastar.IdleMiner.Manager;
using System.Linq;
using UnityEngine;

namespace Komastar.IdleMiner.UI
{
    public class UIGearPresenter : MonoBehaviour
    {
        [SerializeField]
        private UIGearView gearViewPrefab;
        [SerializeField]
        private Transform contentTransform;

        [SerializeField]
        private GameObject gearScrollView;
        private bool isSelect;
        private UISelectResponse<GearDO> selectedGear;

        private RectTransform ownRectTransform;
        private Vector2 openedRectSize;
        private Vector2 closedRectSize;

        public bool IsOpen => gearScrollView.activeSelf;

        private void Awake()
        {
            ownRectTransform = GetComponent<RectTransform>();
            openedRectSize = ownRectTransform.sizeDelta;
            closedRectSize = new Vector2(openedRectSize.x, 0f);
            ownRectTransform.sizeDelta = closedRectSize;
            gearScrollView.SetActive(false);

            var gears = DataManager.Get().GetAllGears();
            gears = gears.OrderBy(g => g.Id).ToList();
            for (int i = 0; i < gears.Count; i++)
            {
                var gearView = Instantiate(gearViewPrefab, contentTransform);
                gearView.Setup(gears[i]);
                gearView.OnClickGearView += OnClickGearView;
            }
        }

        private void OnClickGearView(GearDO gearData)
        {
            if (isSelect)
            {
                return;
            }

            selectedGear = new UISelectResponse<GearDO>(gearData);
            isSelect = true;
        }

        public async UniTask<UISelectResponse<GearDO>> Open()
        {
            selectedGear = null;
            isSelect = false;

            gearScrollView.SetActive(true);
            await ownRectTransform.DOSizeDelta(openedRectSize, .25f);

            await UniTask.WaitUntil(() => isSelect);

            await ownRectTransform.DOSizeDelta(closedRectSize, .25f);
            gearScrollView.SetActive(false);

            return selectedGear;
        }

        public void Close()
        {
            selectedGear = default;
            isSelect = true;
        }
    }
}