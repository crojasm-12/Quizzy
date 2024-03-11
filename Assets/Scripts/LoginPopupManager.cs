using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;


public class LoginPopupManager : MonoBehaviour
{

    private LoginPanel loginPanel;
    private ForgotPasswordPanel forgotPasswordPanel;

    void Start()
    {
        loginPanel.ShowPanel();
        forgotPasswordPanel.HidePanel();
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Awake() // It's better to use Awake to ensure that references are set before any Start methods call them
    {
        loginPanel = FindFirstObjectByType<LoginPanel>();
        forgotPasswordPanel = FindFirstObjectByType<ForgotPasswordPanel>();

        if (loginPanel == null || forgotPasswordPanel == null)
        {
            Debug.LogError("PanelController instances not found in the scene!");
        }
    }

}
