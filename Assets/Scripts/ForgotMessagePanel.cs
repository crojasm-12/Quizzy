using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgotMessagePanel : MonoBehaviour
{
    private LoginPanel loginPanel;

    public GameObject Panel;

    void Awake()
    {
        loginPanel = FindFirstObjectByType<LoginPanel>();

        if (loginPanel == null)
        {
            Debug.LogError("LoginPanel instances not found in the ForgotMessagePanel!");
        }
    }

    public void ShowPanel()
    {
        Panel.SetActive(true);
    }

    public void HidePanel()
    {
        Panel.SetActive(false);
    }

    public void OnOK()
    {
        HidePanel();
        loginPanel.ShowPanel();
    }
}
