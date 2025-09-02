using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.Single);
    }

    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.Dual);
    }

    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeToGameScene(Constants.GameType.Multi);
    }

    public void OnClickSettingButton()
    {
        
    }
}
