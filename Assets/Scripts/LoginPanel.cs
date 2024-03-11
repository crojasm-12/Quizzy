using System;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    private ForgotPasswordPanel forgotPasswordPanel;
    private InitialBackgroundPanel initialBackgroundPanel;
    private SignUpPanel signUpPanel;

    public GameObject Panel;
    public TextMeshProUGUI ErrorMessage;
    public TMP_InputField UserId;
    public TMP_InputField Password;

    void Awake() // It's better to use Awake to ensure that references are set before any Start methods call them
    {
        forgotPasswordPanel = FindFirstObjectByType<ForgotPasswordPanel>();
        if (forgotPasswordPanel == null)
        {
            Debug.LogError("ForgotPasswordPanel instances not found in LoginPanel!");
        }
        initialBackgroundPanel = FindFirstObjectByType<InitialBackgroundPanel>();
        if (initialBackgroundPanel == null)
        {
            Debug.LogError("InitialBackgroundPanel instances not found in LoginPanel!");
        }
        signUpPanel = FindFirstObjectByType<SignUpPanel>();
        if (signUpPanel == null)
        {
            Debug.LogError("SignUpPanel instances not found in LoginPanel!");
        }
    }

    public void ShowPanel()
    {
        Panel.SetActive(true);
        InitErrorMessage();
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
        InitErrorMessage();
    }

    public void ShowError(string message)
    {
        if (ErrorMessage != null)
        {
            ErrorMessage.text = message;
            ErrorMessage.gameObject.SetActive(true);
        }
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
            ShowError("NickName and Password cannot be Empty.");
            return;
        }


        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();

        string jsonPayload = $"{{ \"email\": \"{UserId.text}\", \"password\": \"{Password.text}\" }}";
        StartCoroutine(httpService.Post("/api/idm/v1/logInLocal", null, jsonPayload, (response) =>
        {
            try
            {
                UserData userData = JsonUtility.FromJson<UserData>(response);

                DataStorage.Instance.StoreData("UserData", userData);
                Debug.Log("Response: " + DataStorage.Instance.GetData<UserData>("UserData"));
                HidePanel();
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                initialBackgroundPanel.HidePanel();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        },
            (error) =>
            {
                ShowError("Invalid NickName or Password.");
            }));
    }

    private void InitErrorMessage()
    {
        if (ErrorMessage != null)
        {
            ErrorMessage.text = String.Empty;
            ErrorMessage.gameObject.SetActive(false);
        }
    }

}
