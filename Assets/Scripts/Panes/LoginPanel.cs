using System;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    private ToastPanel toastPanel;
    private MainScene mainScene;

    private ForgotPasswordPanel forgotPasswordPanel;
    private SignUpPanel signUpPanel;

    private InitialBackgroundPanel initialBackgroundPanel;

    public GameObject Panel;
    public TMP_InputField UserId;
    public TMP_InputField Password;

    void Awake()
    {
        mainScene = FindFirstObjectByType<MainScene>();
        forgotPasswordPanel = FindFirstObjectByType<ForgotPasswordPanel>();
        initialBackgroundPanel = FindFirstObjectByType<InitialBackgroundPanel>();
        signUpPanel = FindFirstObjectByType<SignUpPanel>();
        toastPanel = FindFirstObjectByType<ToastPanel>();
    }

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void OnSignUp()
    {
        HidePanel();
        signUpPanel.ShowPanel();
    }

    public void OnForgot()
    {
        HidePanel();
        forgotPasswordPanel.ShowPanel();
    }

    public void OnLogin()
    {
        if (UserId.text.Length == 0 || Password.text.Length == 0)
        {
            toastPanel.ShowError("NickName and Password cannot be Empty.");
            return;
        }

        HttpService httpService;
        httpService = gameObject.AddComponent<HttpService>();
        string jsonPayload = $"{{ \"email\": \"{UserId.text}\", \"password\": \"{Password.text}\" }}";
        StartCoroutine(httpService.Post("/api/idm/v1/logInLocal", null, jsonPayload,
        (response) =>
        {
            try
            {
                UserData userData = JsonUtility.FromJson<UserData>(response);
                DataStorage.Instance.StoreData("UserData", userData);
                HidePanel();
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                initialBackgroundPanel.HidePanel();
                mainScene.ShowPanel();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        },
        (error) =>
        {
            toastPanel.ShowError("Invalid NickName or Password.");
        }));
    }

}
