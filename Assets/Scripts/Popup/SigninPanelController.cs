using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public struct SigninData
{
    public string username;
    public string password;
}

public struct SigninResult
{
    public int result;
}

public class SigninPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Shake();
            return;
        }
        
        SigninData signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        StartCoroutine(NetworkManager.Instance.Signin(signinData,
            () =>
            {
                GameManager.Instance.OpenConfirmPanel("로그인 성공", 
                    ()=>{
                        Hide();
                    });
            },
            (int result) =>
            {
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("유저네임이 유효하지 않습니다.", 
                        ()=>
                        {
                            usernameInputField.text = "";
                            passwordInputField.text = "";
                        });
                            }
                else if (result == 1)
                {
                    GameManager.Instance.OpenConfirmPanel("비밀번호가 유효하지 않습니다.", 
                        () =>
                        {
                            passwordInputField.text = "";
                        });
                }
            }));
    }
}
