using UnityEngine;
using UnityEngine.UI;

public class UIOpenPanelAsyncSample : MonoBehaviour
{
    public UIContentPanelSample samplePanel;
    public Text outputText;
    public Button openButton;

    public async void OnClickOpenPanelAsync()
    {
        var result = await samplePanel.OpenAsync();
        if (!string.IsNullOrEmpty(result))
        {
            outputText.text = result;
        }
    }

    public void OnClickOpenPanelLegacy()
    {
        samplePanel.OpenLegacy((result) =>
        {
            if (!string.IsNullOrEmpty(result))
            {
                outputText.text = result;
            }
        });
    }
}
