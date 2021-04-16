using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIContentPanelSample : MonoBehaviour
{
    private bool isClicked;
    private string clickedButtonString;

    private UnityAction<string> onClickButton;

    public Button buttonPrefab;

    private void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            var newButton = Instantiate(buttonPrefab, transform);
            string buttonContent = $"{i + 1}";
            newButton.GetComponentInChildren<Text>().text = buttonContent;
            newButton.onClick.AddListener(() =>
            {
                clickedButtonString = buttonContent;
                isClicked = true;
                onClickButton?.Invoke(buttonContent);
            });
        }
        Close();
    }

    public async UniTask<string> OpenAsync()
    {
        if (!gameObject.activeSelf)
        {
            isClicked = false;
            clickedButtonString = "";
            gameObject.SetActive(true);

            await UniTask.WaitUntil(() => isClicked);
        }

        Close();

        return clickedButtonString;
    }

    public void OpenLegacy(UnityAction<string> onComplete)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            onClickButton = null;
            onClickButton += onComplete;
            onClickButton += (s) => { Close(); };
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        isClicked = false;
        gameObject.SetActive(false);
    }
}
