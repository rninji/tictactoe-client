using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    // Confirm 버튼 클릭 시 호출될 Delegate
    public delegate void OnConfirmButtonClicked();

    private OnConfirmButtonClicked _onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked)
    {
        messageText.text = message;
        _onConfirmButtonClicked = onConfirmButtonClicked;
        
        base.Show();
    }
    
    public void OnClickConfirmButton()
    {
        Hide(()=>
        {
            _onConfirmButtonClicked?.Invoke();
        });
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
