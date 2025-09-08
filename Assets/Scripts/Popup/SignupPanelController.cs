using TMPro;
using UnityEngine;

public struct SignupData
{
    public string username;
    public string password;
    public string nickname;
}

public struct SignupResult
{
    public int result;
}

public class SignupPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = passwordInputField.text;
        string nickname = passwordInputField.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname))
        {
            Shake();
            return;
        }
        
        // Confirm Password
        if (password.Equals(confirmPassword))
        {
            
        }
        
        SignupData signupData = new SignupData();
        signupData.username = username;
        signupData.password = password;
        signupData.nickname = nickname;

        StartCoroutine(NetworkManager.Instance.Signup(signupData,
            () =>
            {
                GameManager.Instance.OpenConfirmPanel("회원가입 성공", 
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
            }));
    }

    public void OnClickCancelButton()
    {
        Hide();
    }
}
