using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UserInformation
{
    public string firstName;
    public string lastName;
    public string nickName;
    public string email;
    public string password;
    public string reconfirmPassword;
}

public class SignUpPanel : MonoBehaviour
{
    private LoginPanel loginPanel;

    public GameObject Panel;
    public TextMeshProUGUI ErrorMessage;
    public TMP_InputField FirstName;
    public TMP_InputField LastName;
    public TMP_InputField NickName;
    public TMP_InputField EMail;
    public TMP_InputField Password1;
    public TMP_InputField Password2;

    void Awake()
    {
        loginPanel = FindFirstObjectByType<LoginPanel>();

        if (loginPanel == null)
        {
            Debug.LogError("LoginPanel instances not found in the SignUpPanel!");
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

    public bool IsValidInputs()
    {
        if (string.IsNullOrEmpty(FirstName.text))
        {
            ShowError("Invalid First Name. Enter First Name.");
            return false;
        }
        if (string.IsNullOrEmpty(LastName.text))
        {
            ShowError("Invalid Last Name. Enter Last Name.");
            return false;
        }
        if (string.IsNullOrEmpty(NickName.text))
        {
            ShowError("Invalid Nickname. Enter Nickname.");
            return false;
        }
        if (!ToolKit.Instance.IsValidEmail(EMail.text))
        {
            ShowError("Invalid EMail Address. Enter EMail.");
            return false;
        }
        if (!ToolKit.Instance.IsValidPassword(Password1.text) || !ToolKit.Instance.IsValidPassword(Password2.text))
        {
            ShowError("The password must be at least 8 characters long and include one uppercase letter, one lowercase letter, and one number.");
            return false;
        }
        if (Password1.text != Password2.text)
        {
            ShowError("Passwords are different. Enter same password.");
            return false;
        }
        return true;
    }

    public void OnSignUp()
    {
        if (!IsValidInputs())
        {
            return;
        }


        HttpService httpService;

        httpService = gameObject.AddComponent<HttpService>();

        UserInformation userInformation = new UserInformation
        {
            firstName = FirstName.text,
            lastName = LastName.text,
            nickName = NickName.text,
            email = EMail.text,
            password = Password1.text,
            reconfirmPassword = Password2.text
        };

        string jsonPayload = JsonUtility.ToJson(userInformation);

        StartCoroutine(httpService.Post("/api/idm/v1/registerLocal", null, jsonPayload,
            (response) =>
            {
                try
                {
                    HidePanel();
                    loginPanel.ShowPanel();
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            },
            (error) =>
            {
                ShowError("Invalid NickName or Password.");
            }
        ));

    }
    public void OnHaveAccount()
    {
        HidePanel();
        loginPanel.ShowPanel();
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
