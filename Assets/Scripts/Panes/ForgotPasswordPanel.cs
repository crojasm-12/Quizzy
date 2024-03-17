using NUnit.Framework;
using System.Net;
using TMPro;
using UnityEngine;

public class ForgotPasswordPanel : MonoBehaviour
{
    private ToastPanel toastPanel;
    private LoginPanel loginPanel;

    public GameObject Panel;
    public TMP_InputField EMailAddress;

    void Awake()
    {
        toastPanel = FindFirstObjectByType<ToastPanel>();
        loginPanel = FindFirstObjectByType<LoginPanel>();
    }

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void OnSendEmail()
    {
        if (!ToolKit.Instance.IsValidEmail(EMailAddress.text))
        {
            toastPanel.ShowError("Invalid EMail address.");
            return;
        }
        toastPanel.ShowMessage("An email has been sent to the address provided.Please check your inbox.");
        HidePanel();
        loginPanel.ShowPanel();
    }

    public void OnHaveAccount()
    {
        HidePanel();
        loginPanel.ShowPanel();
    }
}
