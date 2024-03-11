using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ForgotPasswordPanel : MonoBehaviour
{
    private LoginPanel loginPanel;
    private ForgotMessagePanel forgotMessagePanel;

    public GameObject Panel;
    public TMP_InputField EMailAddress;
    public TextMeshProUGUI ErrorMessage;


    void Awake()
    {
        forgotMessagePanel = FindFirstObjectByType<ForgotMessagePanel>();
        if (forgotMessagePanel == null)
        {
            Debug.LogError("ForgotMessagePanel instances not found in the ForgotPasswordPanel!");
        }
        loginPanel = FindFirstObjectByType<LoginPanel>();
        if (loginPanel == null)
        {
            Debug.LogError("LoginPanel instances not found in the ForgotPasswordPanel!");
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

    public void OnSendEmail()
    {
        if (!ToolKit.Instance.IsValidEmail(EMailAddress.text))
        {
            ShowError("Invalid EMail address.");
            return;
        }
        HidePanel();
        forgotMessagePanel.ShowPanel();
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
