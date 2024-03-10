using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;


public class LoginPopupManager : MonoBehaviour
{
    public GameObject Popup_Login;
    public GameObject Popup_Forgot;

    public TMP_InputField emailAddress;

    public TMP_InputField userId;
    public TMP_InputField password;

    public TextMeshProUGUI Login_Error_Message;
    public TextMeshProUGUI Forgot_Error_Message;

    void Start()
    {
        ShowLoginPopup();
        ShowForgotPopup(false);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void ShowLoginPopup(bool show = true)
    {
        if (Popup_Login != null)
        {
            Popup_Login.SetActive(show);
        }
        if (Login_Error_Message != null)
        {
            Login_Error_Message.text = String.Empty;
            Login_Error_Message.gameObject.SetActive(false);
            return;
        }
    }

    private void ShowLoginError(string error)
    {
        if (Login_Error_Message != null)
        {
            Login_Error_Message.text = error;
            Login_Error_Message.gameObject.SetActive(true);
        }
    }

    private void ShowForgotPopup(bool show = true)
    {
        if (Popup_Forgot != null)
        {
            Popup_Forgot.SetActive(show);
        }
        if (Forgot_Error_Message != null)
        {
            Forgot_Error_Message.text = String.Empty;
            Forgot_Error_Message.gameObject.SetActive(false);
        }
    }
    
    private void ShowForgotError(string error)
    {
        if (Forgot_Error_Message != null)
        {
            Forgot_Error_Message.text = error;
            Forgot_Error_Message.gameObject.SetActive(true);
        }
    }

    public void OnForgot()
    {
        ShowLoginPopup(false);
        ShowForgotPopup();
    }

    public void OnHaveAccount()
    {
        ShowForgotPopup(false);
        ShowLoginPopup();
    }

    public void OnSendEmail()
    {
        if (!IsValidEmail(emailAddress.text))
        {
            ShowForgotError("Invalid EMail address.");
            return;
        }
        Debug.Log(emailAddress.text);
        ShowForgotPopup(false);
        ShowLoginPopup();
    }

    public void OnLogin()
    {
        if (userId.text.Length == 0 || password.text.Length == 0)
        {
            ShowLoginError("NickName and Password cannot be Empty.");
            Login_Error_Message.gameObject.SetActive(true);
            return;
        }


        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();
        httpService.Initialize("https://api.tomorrow-notebook.com");

        string jsonPayload = $"{{ \"email\": \"{userId.text}\", \"password\": \"{password.text}\" }}";
        StartCoroutine(httpService.Post("/api/idm/v1/logInLocal", null, jsonPayload, (response) =>
            {
                try
                {
                    UserData userData = JsonUtility.FromJson<UserData>(response);

                    DataStorage.Instance.StoreData("UserData", userData);
                    Debug.Log("Response: " + DataStorage.Instance.GetData<UserData>("UserData"));
                    ShowLoginPopup(false);
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            },
            (error) =>
            {
                ShowLoginError("Invalid NickName or Password.");
            }));
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(email, emailPattern);
    }
}
