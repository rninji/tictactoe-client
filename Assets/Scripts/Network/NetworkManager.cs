using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : Singleton<NetworkManager>
{
    
    // 로그인
    public IEnumerator Signin(SigninData signinData, Action success, Action<int> failure)
    {
        string jsonString = JsonUtility.ToJson(signinData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerUrl+"/users/signin", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // TODO: 서버 연결 오류 에러 알림
            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);

                if (result.result == 2)
                {
                    // 로그인 성공
                    var cookie = www.GetRequestHeader("set-cookie");
                    if (!string.IsNullOrEmpty(cookie))
                    {
                        int lastIndex = cookie.LastIndexOf(';');
                        string sid = cookie.Substring(0, lastIndex);
                        
                        // 파일로 저장
                        PlayerPrefs.SetString("sid", sid);
                        
                    }
                    
                    success?.Invoke();
                }
                else
                {
                    failure?.Invoke(result.result);
                }
            }
        }
    }
    
    // 회원가입
    public IEnumerator Signup(SignupData signupData, Action success, Action<int> failure)
    {
        string jsonString = JsonUtility.ToJson(signupData);
        byte[] byteRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www = new UnityWebRequest(Constants.ServerUrl+"/users/signup", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(byteRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                // TODO: 서버 연결 오류 에러 알림
            }
            else
            {
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SignupResult>(resultString);

                if (result.result == 2)
                {
                    // 회원가입 성공
                    success?.Invoke();
                }
                else
                {
                    failure?.Invoke(result.result);
                }
            }
        }
    }
    
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        
    }
}
